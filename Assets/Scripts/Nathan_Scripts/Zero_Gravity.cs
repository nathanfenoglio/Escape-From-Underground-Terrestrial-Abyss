using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zero_Gravity : MonoBehaviour
{
    public bool transport_too = true;
    public Transform transport_to_here;

    //specify amount in seconds before gravity exists again
    [SerializeField] private float how_long_before_reset;

    //need boolean for disallowing player to collide again and again, it's either on or off but not on and then on again
    private bool on = false;
    //need boolean for disallowing OnCollisionExit2D from starting the timer when the timer is already running
    private bool timer_currently_running = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null) {
            Static_Vars_1.zero_gravity = true;

            if (transport_too) {
                player.transform.position = new Vector3(transport_to_here.position.x, transport_to_here.position.y, transport_to_here.position.z);            
            }

            if (!timer_currently_running)
            {
                timer_currently_running = true;
                Invoke("turn_gravity_off", how_long_before_reset);          
            }
        }
    }

    void turn_gravity_off() {
        Static_Vars_1.zero_gravity = false;
        Transform player = GameObject.Find("Player").transform;
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
        timer_currently_running = false;
        on = false;
    }
}
