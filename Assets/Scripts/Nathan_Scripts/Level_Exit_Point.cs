using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Exit_Point : MonoBehaviour
{
    public float move_x_dir_amount;
    public float move_y_dir_amount;
    private Transform pos;
    public bool need_to_collect_tokens = false;
    public int num_tokens_needed;

    private void Update()
    {
        if (need_to_collect_tokens) {

            if (Static_Vars_1.bubble_boy_kills >= num_tokens_needed) {
                move_position();
            }
        }
    }


    void move_position() {
        transform.position = new Vector3(0, 0, 0);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //should check if collision is with Player, don't want it to load new scene if something else collides with it
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null) {
            SceneManager.LoadScene("Title_Screen");
        }
        
    }
}
