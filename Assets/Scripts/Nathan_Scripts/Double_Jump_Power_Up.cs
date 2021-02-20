using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Double_Jump_Power_Up : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player") {
            Static_Vars_1.double_jump_static = true;
            collision.gameObject.GetComponent<Animator>().SetBool("double_jump_power_up", true);
            Destroy(gameObject);
        }
    }
}
