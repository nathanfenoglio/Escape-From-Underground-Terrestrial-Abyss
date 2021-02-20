using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//really should be titled warp from one spot to another in a room
public class Bimmy_0_1_0__1_1_0 : MonoBehaviour
{
    public Transform place_to_travel_to; //specify coordinates of warp to spot in inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null) {
            player.transform.position = new Vector3(place_to_travel_to.position.x + 1, place_to_travel_to.position.y, place_to_travel_to.position.z);
        }
    }
}
