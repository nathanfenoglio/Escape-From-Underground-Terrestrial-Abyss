using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;

    public int damage = 40;

    public Rigidbody2D rb;

    public bool shootUp = false;

    // Start is called before the first frame update
    void Start()
    {
        //check if player is aiming up or straight ahead
        if (shootUp)
        {
            rb.velocity = transform.up * speed;
        }
        else {
            rb.velocity = transform.right * speed; //right is right no matter if facing left or right right?
        }
        
    }

    //check if bullet is colliding with something
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //things that a bullet can do damage to
        Enemy_1 enemy_1 = hitInfo.GetComponent<Enemy_1>();
        Flyer_Enemy_Movement enemy_flying_1 = hitInfo.GetComponent<Flyer_Enemy_Movement>(); //the flying enemy that follows the player
        Fly_Back_And_Forth enemy_flying_2 = hitInfo.GetComponent<Fly_Back_And_Forth>(); //the flying enemy that just consistently flys back and forth
        Enemy_2 enemy_2 = hitInfo.GetComponent<Enemy_2>(); //the platform patroller
        Block_Shootable block_shootable = hitInfo.GetComponent<Block_Shootable>(); //the block that you can destroy with a shot
        BubbleBoy_1 bubble_boy_1 = hitInfo.GetComponent<BubbleBoy_1>();

        //getting reference to the Player to ignore collision with bullet
        Player the_player = hitInfo.GetComponent<Player>();

        if (enemy_1 != null) {
            enemy_1.TakeDamage(damage);
        }
        if (enemy_flying_1 != null) {
            enemy_flying_1.TakeDamage(damage);
        }
        if (enemy_flying_2 != null) {
            enemy_flying_2.TakeDamage(damage);
        }
        if (enemy_2 != null) {
            enemy_2.TakeDamage(damage);
        }
        if (block_shootable != null) {
            block_shootable.TakeDamage(100); //100 is all of the block's health
        }
        if (bubble_boy_1 != null) {
            bubble_boy_1.TakeDamage(damage);
        }
        //if bullet collides with player, ignore it
        if (the_player != null) {
            return; 
        }

        Destroy(gameObject); //destroy bullet
        
    }

}
