using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Switch_1_Activate : MonoBehaviour
{
    public GameObject platform_1;

    //specify amount left or right or up or down that the platform will move when the switch is activated
    [SerializeField] private float up_down_amount;
    [SerializeField] private float left_right_amount;

    //specify amount in seconds before switch is flipped back and platform returns to original position
    [SerializeField] private float how_long_before_reset;

    //need boolean for disallowing player to collide again and again, it's either on or off but not on and then on again
    private bool on = false;

    //need boolean for disallowing OnCollisionExit2D from calling set_switch_back when the timer is already running
    private bool timer_currently_running = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null && !on) {
            on = true;
            transform.Rotate(0f, 180f, 0f);
            platform_1.transform.position = new Vector3(platform_1.transform.position.x + left_right_amount, platform_1.transform.position.y + up_down_amount, 0);
        }
    }

    //set up timer to return to original state after specified amount of time
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!timer_currently_running)
        {
            timer_currently_running = true;
            Invoke("set_switch_back", how_long_before_reset);
            
        }

    }

    void set_switch_back(){
        transform.Rotate(0f, -180f, 0f);
        platform_1.transform.position = new Vector3(platform_1.transform.position.x - left_right_amount, platform_1.transform.position.y - up_down_amount, 0);
        timer_currently_running = false;
        on = false;
    }
    
}
