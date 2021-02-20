using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSupplement : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null) {

            //check if health bar is already full
            if (Static_Vars_1.player_health_static < 1.0f)
            {
                Static_Vars_1.player_health_static = Static_Vars_1.player_health_static + .08f; //making health increase twice the normal damage that an enemy will cause to the player
                Destroy(gameObject);
            }
            else {
                Destroy(gameObject);
            }
            
        }
    }
}
