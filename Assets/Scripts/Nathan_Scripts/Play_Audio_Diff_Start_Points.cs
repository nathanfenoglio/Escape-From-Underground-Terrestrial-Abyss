using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class Play_Audio_Diff_Start_Points : MonoBehaviour
{
    private static Play_Audio_Diff_Start_Points instance = null;

    private int[] rooms_in_solution_path = { 0, 6, 15, 16, 7, 1, 2, 11, 10, 13, 14, 17, 26, 25, 24, 18, 20 };
    private static bool already_playing = true; //is the track already playing? if it is, don't want to play it again

    private static int room_index_holder;
    
    public static Play_Audio_Diff_Start_Points Instance {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }

        room_index_holder = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        int current_room = SceneManager.GetActiveScene().buildIndex;
        if (current_room != room_index_holder) {
            room_index_holder = current_room;
            bool index_in_sol_path = false;

            foreach (var room_index in rooms_in_solution_path)
            {

                if (room_index == current_room)
                {
                    index_in_sol_path = true;
                }
            }

            if (!index_in_sol_path)
            {
                already_playing = false;
                instance.GetComponent<AudioSource>().Stop();
            }
            else
            {

                if (!already_playing)
                {
                    already_playing = true;
                    instance.GetComponent<AudioSource>().Play();
                }

            }
        
        }
        
    }

}
