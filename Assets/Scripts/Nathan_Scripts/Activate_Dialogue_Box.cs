using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Dialogue_Box : MonoBehaviour
{
    public GameObject Dialogue_Box_1;
    public Joystick joystick;
    public Shoot_Button shoot_button;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Time.timeScale = 0f; //stop game play
            Dialogue_Box_1.SetActive(true); //show dialogue box
        }
    }

    private void Update()
    {
        
        if (Static_Vars_1.mobile_ui)
        {
            joystick = FindObjectOfType<Joystick>();
            shoot_button = FindObjectOfType<Shoot_Button>();

            if (joystick.Vertical > 0.1f) {
                Time.timeScale = 1f;
                Dialogue_Box_1.SetActive(false);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Time.timeScale = 1f; //start game play
            Dialogue_Box_1.SetActive(false); //unshow dialogue box
        }
    }

}
