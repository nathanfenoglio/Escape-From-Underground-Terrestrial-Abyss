using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    private Rigidbody2D myRigidBody; 

    [SerializeField] private float movementSpeed;

    private bool facing_left;

    public Transform groundDetection;

    public float distance = 0.2f; //setting distance of raycast

    public int health = 100;

    private float normal_damage_to_cause = 0.04f; //damage caused to player

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        facing_left = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
        
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        //check for raycast to the right and left to see if colliding with a wall or door or something, so turn around
        RaycastHit2D leftInfo = Physics2D.Raycast(groundDetection.position, Vector2.left, 0.02f);
        RaycastHit2D rightInfo = Physics2D.Raycast(groundDetection.position, Vector2.right, 0.02f);

        Vector3 rb_velocity = myRigidBody.velocity;

        if (groundInfo.collider == false) //check if raycast does not collide with anything 
        //adding check to the right for some reason makes it get stuck less often it seems, but still gets stuck
        { //check if raycast does not collide with anything 
            //if not colliding, then at an edge, so flip direction
            if (facing_left == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                facing_left = false;
            }
            else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                facing_left = true;
            }
        }

    }

    private void FixedUpdate()
    {
        //get the velocity of the Enemy, but you're not doing anything with it I don't think...
        float horizontal = Input.GetAxis("Horizontal");
        //Flip(horizontal);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Enemy_1" || collision.gameObject.name == "Player" || collision.gameObject.name == "Left_Wall" || collision.gameObject.name == "Right_Wall" || collision.gameObject.name == "Left_Door" || collision.gameObject.name == "Right_Door" || collision.gameObject.name == "Up_Door" || collision.gameObject.name == "Down_Door" || collision.gameObject.name == "Out_Door" || collision.gameObject.name == "In_Door") {

            float start_enemy_dist_outside_of_collider = 0.125f;
           
            if (facing_left == true)
            {
                start_enemy_dist_outside_of_collider = start_enemy_dist_outside_of_collider * -1;
                transform.eulerAngles = new Vector3(0, 0, 0);
                transform.position = transform.position + new Vector3(start_enemy_dist_outside_of_collider, 0, 0);
                facing_left = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                transform.position = transform.position - new Vector3(start_enemy_dist_outside_of_collider, 0, 0);
                facing_left = true;
            }

            //cause player damage
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null)
            {
                //check if player has half damage powerup
                if (Static_Vars_1.half_damage_static)
                {
                    Static_Vars_1.player_health_static = Static_Vars_1.player_health_static - (normal_damage_to_cause / 2);
                }
                else
                {
                    Static_Vars_1.player_health_static = Static_Vars_1.player_health_static - normal_damage_to_cause;
                }

            }
        }
    }

    public void TakeDamage(int damage) {
        health = health - damage;

        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        gameObject.transform.DetachChildren();
        Destroy(gameObject);
    }

}
