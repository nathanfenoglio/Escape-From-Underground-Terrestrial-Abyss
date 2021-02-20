using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer_Enemy_Movement : MonoBehaviour
{
    private Player thePlayer;

    public float moveSpeed;

    public float playerRange;

    public LayerMask playerLayer;

    public bool playerInRange;

    public int health = 100;

    private float normal_damage_to_cause = 0.04f; //damage caused to player

    private bool facing_right;

    // Start is called before the first frame update
    void Start()
    {
        facing_right = true;
        thePlayer = FindObjectOfType<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //checks if player is within the specified range of the enemy
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        //check if player is in specified range of flying enemy's attention
        if (playerInRange) {
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, moveSpeed * Time.deltaTime);
        }

        Flip();
    }

    //drawing gizmo so can see the radius that the enemy will notice the player within for debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRange);
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
        //move enemy away from player 
        transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, 0);
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

    void Flip()
    {
        Transform player = GameObject.Find("Player").transform;
        if (player.position.x > transform.position.x && !facing_right || player.position.x < transform.position.x && facing_right)
        {
            facing_right = !facing_right;
            transform.Rotate(0, 180f, 0);
        }
    }
}
