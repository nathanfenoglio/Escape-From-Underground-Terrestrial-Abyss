using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_Audio_No_Restart_Path_2 : MonoBehaviour
{
    private static Play_Audio_No_Restart_Path_2 instance = null;

    private int[] rooms_in_path = { 3, 12, 9 };
    private static bool already_playing_2 = true; //is the track already playing? if it is, don't want to play it again

    private static int room_index_holder_2;

    public static Play_Audio_No_Restart_Path_2 Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        
        room_index_holder_2 = SceneManager.GetActiveScene().buildIndex;

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        int current_room = SceneManager.GetActiveScene().buildIndex;
        
        if (current_room != room_index_holder_2)
        {
            room_index_holder_2 = current_room;
            bool index_in_sol_path = false;

            foreach (var room_index in rooms_in_path)
            {

                if (room_index == current_room)
                {
                    index_in_sol_path = true;
                }
            }

            if (!index_in_sol_path)
            {
                already_playing_2 = false;
                instance.GetComponent<AudioSource>().Stop();
            }
            else
            {

                if (!already_playing_2)
                {
                    already_playing_2 = true;
                    instance.GetComponent<AudioSource>().Play();
                }

            }
        }
    }
}
