using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_General : MonoBehaviour
{
    public Transform place_to_travel_to; //place to warp to
    
    public float x_offset; //post warp position offset x
    public float y_offset; //post warp position offset y

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.transform.position = new Vector3(place_to_travel_to.position.x + x_offset, place_to_travel_to.position.y + y_offset, place_to_travel_to.position.z);
        }
    }
}
