using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Half_Damage_Power_Up : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Static_Vars_1.half_damage_static = true;
            Destroy(gameObject);
        }
    }
}
