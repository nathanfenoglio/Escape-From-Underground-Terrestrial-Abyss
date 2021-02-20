using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidBody;

    public Joystick joystick;

    [SerializeField] private float movementSpeed;

    private bool facing_right;

    [SerializeField] private Transform[] groundPoints;

    [SerializeField] private float groundRadius;

    [SerializeField] private LayerMask whatIsGround;

    private bool isGrounded;

    private bool jump;

    [SerializeField] private float jumpForce;

    [SerializeField] private bool airControl;

    [SerializeField] private float airMovementSpeed; //currently set in the inspector

    public Level_Controller_1 level_controller_1;

    public bool OnLadder;
    [SerializeField]
    private float climbSpeed = 2;

    //get reference to child GunBarrel object to transform it instead of player
    public GameObject barrel_ref;
    public GameObject barrel_ref_up;
    public GameObject barrel_ref_down;

    //***ALLOW ABILITY FOR A DOUBLE JUMP*** in Static_Vars_1 script
    //# of double jump midair opportunities remaining variable to track
    private int midair_jumps_remaining;
    //time before midair jump is allowed
    [SerializeField] private int how_soon_to_midair_jump;

    //health bar
    public HealthBar healthBar;

    //***HEALTH IS SAVED IN Static_Vars_1 script***

    //crouching sprite image
    public Sprite crouching_sprite_1;

    public BoxCollider2D box_col_orig_size;
    float box_col_offset_y;

    public bool is_crouching = false;

    public bool shoot_up;

    //reference to different player idle image sprites to change to (using animations instead now)
    [SerializeField]
    private Sprite idle_image_before_shot_powerup;
    [SerializeField]
    private Sprite idle_image_after_shot_powerup;
    private SpriteRenderer sprite_renderer;

    private Animator animator; //reference to animator to transition into the various animations

    void Start()
    {
        if (Static_Vars_1.start_game) {
            Static_Vars_1.start_game = false;
            SceneManager.LoadScene("Title_Screen"); //load title screen if first beginning game, Unity starts with scene at index 0, but start screen is at index 27
        }
        //for debugging purposes adding keys to inventory manually, NEED TO REMOVE FOR ACTUAL GAME PLAY
        /*
        Static_Vars_1.door_keys.Add("Key_AA");
        Static_Vars_1.door_keys.Add("Key_BB");
        Static_Vars_1.door_keys.Add("Key_CC");
        Static_Vars_1.door_keys.Add("Key_DD");
        */
        animator = GetComponent<Animator>();

        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();

        //check if player has first shot powerup to change sprite on
        if (Static_Vars_1.double_jump_static)
        {
            animator.SetBool("double_jump_power_up", true);
        }
        else if (Static_Vars_1.regular_shot || Static_Vars_1.spray_shot_static) {
            sprite_renderer.sprite = idle_image_after_shot_powerup;
            animator.SetBool("shot_power_up", true);
        }
        else
        {
            sprite_renderer.sprite = idle_image_before_shot_powerup;
        }

        //checking if starting from load saved data to specify player position in room
        if (Static_Vars_1.is_loading) {
            Static_Vars_1.is_loading = false;
            //set player position
            gameObject.transform.position = new Vector3(Static_Vars_1.player_position_x, Static_Vars_1.player_position_y, 0f);
        }

        //getting reference to MOBILE UI controls
        if (Static_Vars_1.mobile_ui)
        {
            joystick = FindObjectOfType<Joystick>();
        }

        how_soon_to_midair_jump = 5;

        healthBar.SetSize(Static_Vars_1.player_health_static);
        healthBar.SetColor(Color.magenta); //change health bar color here or change to discrete units to display...

        //get reference to Player object's RigidBody2D component
        myRigidBody = GetComponent<Rigidbody2D>();

        //get reference to the 3 gun barrels
        barrel_ref = GameObject.Find("GunBarrel");
        barrel_ref_up = GameObject.Find("GunBarrel10DegUp");
        barrel_ref_down = GameObject.Find("GunBarrel10DegDown");

        facing_right = true;

        OnLadder = false;

        //accessing level_controller_1 script for room loading door logic
        level_controller_1 = GameObject.FindObjectOfType(typeof(Level_Controller_1)) as Level_Controller_1;

        try
        {
            level_controller_1.place_player_in_next_scene(); //checking and noting if the 6 possible doors exist
        }
        catch {
            Debug.Log("Not ready yet or something: level_controller_1.place_player_in_next_scene()");
        }

        midair_jumps_remaining = 1; //could perhaps modify if there would be occasions when you would want to add more than 1 midair jump
    }

    // Update is called once per frame
    void Update()
    {
        if (Static_Vars_1.mobile_ui)
        {
            HandleMobileInput();
        }
        else {
            HandleInput();
        }

    }

    private void FixedUpdate()
    {
        float horizontal;
        float vertical;

        //check if mobile ui or keyboard input
        if (Static_Vars_1.mobile_ui)
        {
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;
        }
        else {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        HandleMovement(horizontal, vertical);

        Flip(horizontal);

        isGrounded = IsGrounded();

        //update health bar display (health deduction is already taken care of in various enemy scripts
        healthBar.SetSize(Static_Vars_1.player_health_static);

        //check health, if at <= 0 returns Player to previous savepoint room restore health, keeps keys and power ups
        if (Static_Vars_1.player_health_static <= 0) {
            Static_Vars_1.player_health_static = 1f;

            //ALEX EDIT
            //I Changed SceneManager.LoadScene(Static_Vars_1.last_saved_room) to SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex)
            //*************************************************************************************************************************
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //also need to save x/y coordinates for checkpoints that don't involve doors to be able to place the player back at here
        }

        ResetValues(); //reset values for next round of checks
    }

    private void HandleMobileInput()
    {
        if (joystick.Vertical < -0.5f && !is_crouching)
        {
            animator.SetBool("crouch", true);
            shrink_collider_crouch();
        }
        //reset
        else if (joystick.Vertical >= -0.5f && is_crouching)
        {
            animator.SetBool("crouch", false);
            unshrink_collider_uncrouch();
        }
    }

    //function for mobile ui jump button
    public void Jump_Input_From_Button() {
        jump = true;
    }

    //function for mobile ui shoot up button
    public void Shoot_Up_From_Button() {
        shoot_up = true;
        barrel_ref.transform.Rotate(0f, 0f, 90f);
        barrel_ref_up.transform.Rotate(0f, 0f, 90f);
        barrel_ref_down.transform.Rotate(0f, 0f, 90f);
    }

    //NOT USING CROUCH FROM BUTTON ANYMORE, USES JOYSTICK DOWN
    public void Crouch_From_Button() {
        //is_crouching = true; //to find and check in Weapon script
        //toggle instead
        if (is_crouching)
        {
            is_crouching = !is_crouching;
        }
        else {
            is_crouching = true;
        }

        if (is_crouching)
        {
            sprite_renderer.sprite = crouching_sprite_1;

            box_col_orig_size = GetComponent<BoxCollider2D>();

            box_col_orig_size.offset = new Vector2(box_col_orig_size.offset.x, box_col_orig_size.offset.y - (box_col_orig_size.size.y * 0.25f));

            box_col_orig_size.size = new Vector2(box_col_orig_size.size.x, box_col_orig_size.size.y * 0.5f);
        }
        else {

            //choose sprite based on if she has regular shot powerup or not
            if (Static_Vars_1.regular_shot || Static_Vars_1.spray_shot_static)
            {
                sprite_renderer.sprite = idle_image_after_shot_powerup;
            }
            else
            {
                sprite_renderer.sprite = idle_image_before_shot_powerup;
            }

            //order matters with undoing the collider shrinkage, have to do the operations in reverse to undo and return to normal standing collider size and position
            box_col_orig_size.size = new Vector2(box_col_orig_size.size.x, box_col_orig_size.size.y * 2f);

            box_col_orig_size.offset = new Vector2(box_col_orig_size.offset.x, box_col_orig_size.offset.y + (box_col_orig_size.size.y * 0.25f));
        }

    }

    private void shrink_collider_crouch() {

        if (!is_crouching) {
            sprite_renderer.sprite = crouching_sprite_1;

            box_col_orig_size = GetComponent<BoxCollider2D>();

            box_col_orig_size.offset = new Vector2(box_col_orig_size.offset.x, box_col_orig_size.offset.y - (box_col_orig_size.size.y * 0.25f));

            box_col_orig_size.size = new Vector2(box_col_orig_size.size.x, box_col_orig_size.size.y * 0.5f);

            is_crouching = true; //to find and check in Weapon script
        }

    }

    private void unshrink_collider_uncrouch() {

        if (is_crouching) {

            //choose sprite based on if she has regular shot powerup or not
            if (Static_Vars_1.regular_shot || Static_Vars_1.spray_shot_static)
            {
                sprite_renderer.sprite = idle_image_after_shot_powerup;
            }
            else
            {
                sprite_renderer.sprite = idle_image_before_shot_powerup;
            }

            //order matters with undoing the collider shrinkage, have to do the operations in reverse to undo and return to normal standing collider size and position
            box_col_orig_size.size = new Vector2(box_col_orig_size.size.x, box_col_orig_size.size.y * 2f);

            box_col_orig_size.offset = new Vector2(box_col_orig_size.offset.x, box_col_orig_size.offset.y + (box_col_orig_size.size.y * 0.25f));

            is_crouching = false;
        }

    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        //check if shooting up
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            barrel_ref.transform.Rotate(0f, 0f, 90f);
            barrel_ref_up.transform.Rotate(0f, 0f, 90f);
            barrel_ref_down.transform.Rotate(0f, 0f, 90f);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            barrel_ref.transform.Rotate(0f, 0f, -90f);
            barrel_ref_up.transform.Rotate(0f, 0f, -90f);
            barrel_ref_down.transform.Rotate(0f, 0f, -90f);
        }

        //change sprite to crouching sprite if down key is pressed
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            is_crouching = true; //to find and check in Weapon script

            sprite_renderer.sprite = crouching_sprite_1;

            animator.SetBool("crouch", true);

            box_col_orig_size = GetComponent<BoxCollider2D>();

            box_col_orig_size.offset = new Vector2(box_col_orig_size.offset.x, box_col_orig_size.offset.y - (box_col_orig_size.size.y * 0.25f));

            box_col_orig_size.size = new Vector2(box_col_orig_size.size.x, box_col_orig_size.size.y * 0.5f);

        }

        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            is_crouching = false;
            animator.SetBool("crouch", false);

            //choose which sprite based on shot powerup
            if (Static_Vars_1.regular_shot || Static_Vars_1.spray_shot_static)
            {
                sprite_renderer.sprite = idle_image_after_shot_powerup;
            }
            else
            {
                sprite_renderer.sprite = idle_image_before_shot_powerup;
            }
            //order matters with undoing the collider shrinkage, have to do the operations in reverse to undo and return to normal standing collider size and position
            box_col_orig_size.size = new Vector2(box_col_orig_size.size.x, box_col_orig_size.size.y * 2f);

            box_col_orig_size.offset = new Vector2(box_col_orig_size.offset.x, box_col_orig_size.offset.y + (box_col_orig_size.size.y * 0.25f));
        }
    }

    private void HandleMovement(float horizontal, float vertical)
    {
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        //zero gravity
        if (Static_Vars_1.zero_gravity) {

            myRigidBody.velocity = new Vector2(horizontal * climbSpeed, vertical * climbSpeed);
            myRigidBody.gravityScale = 0;
        }

        if ((isGrounded || airControl)) {

            if (airControl && !isGrounded)
            {
                myRigidBody.velocity = new Vector2(horizontal * airMovementSpeed, myRigidBody.velocity.y);
            }
            else
            {
                myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y); //just grab and keep the y velocity of the object, then multiply the "Horizontal" x component by whatev to control the speed
            }

        }

        //if jumping then you will no longer be grounded, so can't jump anymore at the moment
        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidBody.AddForce(new Vector2(0, jumpForce)); //add force only in y direction for jump
            jump = false; //setting jump false here seems to have solved the problem of the player being able to sometimes jump way high when double jump is on
            animator.SetBool("jump", true);
        }

        //if jumping and in the air
        if (!isGrounded && jump && (myRigidBody.velocity.y < how_soon_to_midair_jump)){ //adjusting how soon you will be able to jump again < 2 feels pretty good
            // ***ALLOW ABILITY FOR A DOUBLE JUMP IF HAVE POWER UP***
            if (Static_Vars_1.double_jump_static && (midair_jumps_remaining > 0))
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0);
                myRigidBody.AddForce(new Vector2(0, jumpForce)); //add force only in y direction for jump
                midair_jumps_remaining = midair_jumps_remaining - 1; //one less jump remaining
            }
        }

        //if isGrounded and not jumping
        if (isGrounded && !jump) {
            midair_jumps_remaining = 1; //ability to double jump (if double jump is true) is available since currently grounded
            animator.SetBool("jump", false);
        }

        //checking if player is on ladder so can go up and down
        if (OnLadder) {
            myRigidBody.velocity = new Vector2(horizontal * climbSpeed, vertical * climbSpeed);
            myRigidBody.gravityScale = 0;
        }


    }

    private void Flip(float horizontal) {
        //if direction has changed
        if (horizontal > 0 && !facing_right || horizontal < 0 && facing_right) {
            facing_right = !facing_right;
            transform.Rotate(0f, 180f, 0f);
        }

    }

    private bool IsGrounded()
    {
        //check if Player is falling down or stopped
        if (myRigidBody.velocity.y <= 0)
        {

            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {

                    //check if any of the colliders are not colliding with the gameObject itself. Can use gameObject keyword as it refers to the gameObject that the script is within - Player
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }

        }
        return false;

    }

    private void ResetValues()
    {
        jump = false;

        if (shoot_up) {
            barrel_ref.transform.Rotate(0f, 0f, -90f);
            barrel_ref_up.transform.Rotate(0f, 0f, -90f);
            barrel_ref_down.transform.Rotate(0f, 0f, -90f);
            shoot_up = !shoot_up;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Up_Door")
        {
            level_controller_1 = GameObject.FindObjectOfType(typeof(Level_Controller_1)) as Level_Controller_1;
            level_controller_1.load_next_scene(0);
        }
        else if (collision.gameObject.name == "Down_Door")
        {
            level_controller_1 = GameObject.FindObjectOfType(typeof(Level_Controller_1)) as Level_Controller_1;
            level_controller_1.load_next_scene(1);
        }
        else if (collision.gameObject.name == "Left_Door")
        {
            level_controller_1 = GameObject.FindObjectOfType(typeof(Level_Controller_1)) as Level_Controller_1;
            level_controller_1.load_next_scene(2);
        }
        else if (collision.gameObject.name == "Right_Door")
        {
            level_controller_1 = GameObject.FindObjectOfType(typeof(Level_Controller_1)) as Level_Controller_1;
            level_controller_1.load_next_scene(3);

        }
        else if (collision.gameObject.name == "Out_Door")
        {
            level_controller_1 = GameObject.FindObjectOfType(typeof(Level_Controller_1)) as Level_Controller_1;
            level_controller_1.load_next_scene(4);
        }
        else if (collision.gameObject.name == "In_Door")
        {
            level_controller_1 = GameObject.FindObjectOfType(typeof(Level_Controller_1)) as Level_Controller_1;
            level_controller_1.load_next_scene(5);
        }
        //check if collision is with ladder
        else if (collision.gameObject.name == "Ladder" || collision.gameObject.name == "Ladder_2" || collision.gameObject.name == "Ladder_2_2" || collision.gameObject.name == "Ladder_2_3" || collision.gameObject.name == "Ladder_2_Right_Collider" || collision.gameObject.name == "Ladder_2_2_Right_Collider" || collision.gameObject.name == "Ladder_2_3_Right_Collider")
        {
            OnLadder = true;
        }

    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ladder" || collision.gameObject.name == "Ladder_2" || collision.gameObject.name == "Ladder_2_2" || collision.gameObject.name == "Ladder_2_3" || collision.gameObject.name == "Ladder_2_Right_Collider" || collision.gameObject.name == "Ladder_2_2_Right_Collider" || collision.gameObject.name == "Ladder_2_3_Right_Collider") {
            myRigidBody.gravityScale = 1;
            OnLadder = false;
        }
    }

}
