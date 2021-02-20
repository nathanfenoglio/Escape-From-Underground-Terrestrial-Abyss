using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_1_1_1_And_Scene_1_1_2_Dialog : MonoBehaviour
{
    public GameObject Dialogue_Box_1;
    public GameObject Dialogue_Box_2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Time.timeScale = 0f;

            //check if player has the key to the door Key_CC
            bool has_key = false;
            foreach (var door_key in Static_Vars_1.door_keys)
            {

                if (door_key == "Key_CC")
                {
                    has_key = true;
                }
            }

            if (has_key)
            {
                Dialogue_Box_2.SetActive(true);
            }
            else
            {
                Dialogue_Box_1.SetActive(true);
            }



        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Time.timeScale = 1f;

            //check if player has the key to the door Key_CC
            bool has_key = false;
            foreach (var door_key in Static_Vars_1.door_keys)
            {

                if (door_key == "Key_CC")
                {
                    has_key = true;
                }
            }

            if (has_key)
            {
                Dialogue_Box_2.SetActive(false);
            }
            else
            {
                Dialogue_Box_1.SetActive(false);
            }

        }
    }
}
