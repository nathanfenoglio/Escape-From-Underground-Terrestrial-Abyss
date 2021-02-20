using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_Back_And_Forth : MonoBehaviour
{
    public float distance = 3;
    public float speed = 2;
    private float start_pos_x;
    private float start_pos_y;
    private bool fly_left_or_down;

    [SerializeField]
    private bool fly_vertical = false; //use to specify whether you want the enemy to fly up and down or left and right, could change during game play based on some event as an option...

    public int health = 100;

    private float normal_damage_to_cause = 0.04f; //damage caused to player

    // Start is called before the first frame update
    void Start()
    {
        //get initial x and y position of enemy
        start_pos_x = transform.position.x;
        start_pos_y = transform.position.y;
        fly_left_or_down = true; //start by flying left or down (change to false if you happen to want to start in the opposite direction
    }

    // Update is called once per frame
    void Update()
    {
        if (!fly_vertical) //fly back and forth horizontally
        {
            if (transform.position.x > start_pos_x - distance && fly_left_or_down)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            else
            {
                fly_left_or_down = false;
            }

            if (transform.position.x < start_pos_x + distance && !fly_left_or_down)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                fly_left_or_down = true;
            }
        }
        else { //fly back and forth vertically
            //same thing as left and right, but instead up and down movement
            if (transform.position.y > start_pos_y - distance && fly_left_or_down)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
            else
            {
                fly_left_or_down = false;
            }

            if (transform.position.y < start_pos_y + distance && !fly_left_or_down)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else
            {
                fly_left_or_down = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
