using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//so that you can count how many bubble boys have been killed to bring on the final exit of the level
public class BubbleBoy_Kill_Token : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null) {
            Static_Vars_1.bubble_boy_kills = Static_Vars_1.bubble_boy_kills + 1;
            Destroy(gameObject);
        }

        Debug.Log("bubble_boy_kills: " + Static_Vars_1.bubble_boy_kills);
    }
}
