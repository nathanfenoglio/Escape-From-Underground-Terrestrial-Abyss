using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes_1 : MonoBehaviour
{
    private float normal_damage_to_cause = 0.04f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
