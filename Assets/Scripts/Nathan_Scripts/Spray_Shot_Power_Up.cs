using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray_Shot_Power_Up : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if collision is with Player
        if (collision.gameObject.name == "Player")
        {
            Static_Vars_1.spray_shot_static = true;
            collision.gameObject.GetComponent<Animator>().SetBool("shot_power_up", true);
            Destroy(gameObject);
        }
        
    }
}
