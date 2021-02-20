using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private float normal_damage_to_cause = 1.00f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        Static_Vars_1.player_health_static = Static_Vars_1.player_health_static - normal_damage_to_cause;
    }
}
