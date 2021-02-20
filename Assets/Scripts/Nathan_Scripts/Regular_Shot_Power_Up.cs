using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular_Shot_Power_Up : MonoBehaviour
{
    public Sprite shooting_sprite;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player") {
            Static_Vars_1.regular_shot = true;
            //set player's sprite to powerup image
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = shooting_sprite;
            collision.gameObject.GetComponent<Animator>().SetBool("shot_power_up", true);
            Destroy(gameObject);
        }
    }
}
