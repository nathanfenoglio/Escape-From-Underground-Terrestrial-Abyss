using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Controller_1 : MonoBehaviour
{
    private Transform player_position;

    private Transform up_door_position;
    private Transform down_door_position;
    private Transform left_door_position;
    private Transform right_door_position;
    private Transform out_door_position;
    private Transform in_door_position;

    private Transform test_door_position;


    //need to make coming_out_of_door static so that it persists otherwise not able to access each time new room is loaded
    public static int coming_out_of_door;

    public void place_player_in_next_scene()
    {
        Debug.Log("coming_out_of_door: " + coming_out_of_door);
        //like if you were leaving the left door, you would come in from the right door and vice versa
        //if leaving the up door, then you would come in the down door
        //if leaving the in door, then you would come in the out door
        //so come in from the opposite door that you exited the last room from
        /*
        up - 0 
        down / bottom - 1 
        left - 2 
        right - 3 
        out - 4 
        in - 5 
         */
        //to oppositize: if door index is odd, subtract one. else, add one
        if (coming_out_of_door % 2 == 1)
        {
            coming_out_of_door = coming_out_of_door - 1;
        }
        else {
            coming_out_of_door = coming_out_of_door + 1;
        }

        string exit_door_name = "";

        //looks like it needs to be a string, just converting
        switch (coming_out_of_door) {
            case 0:
                exit_door_name = "Up_Door";
                break;
            case 1:
                exit_door_name = "Down_Door";
                break;
            case 2:
                exit_door_name = "Left_Door";
                break;
            case 3:
                exit_door_name = "Right_Door";
                break;
            case 4:
                exit_door_name = "Out_Door";
                break;
            case 5:
                exit_door_name = "In_Door";
                break;
            default:
                Debug.Log("Something unexpected happened");
                break;
        }
        //find coordinates of door that Player is exiting
        test_door_position = GameObject.Find(exit_door_name).transform;

        //change Player's position to outside of door collider 
        player_position = GameObject.Find("Player").transform;

        if (exit_door_name == "Right_Door")
        {
            player_position.position = new Vector3(test_door_position.transform.position.x - 1, test_door_position.transform.position.y, 0);
        }
        else if (exit_door_name == "Down_Door") {
            player_position.position = new Vector3(test_door_position.transform.position.x + 1, test_door_position.transform.position.y + 3, 0);
        }
        else if (exit_door_name == "Up_Door")
        {
            player_position.position = new Vector3(test_door_position.transform.position.x + 1, test_door_position.transform.position.y - 3, 0);
        }
        else
        {
            player_position.position = new Vector3(test_door_position.transform.position.x + 1, test_door_position.transform.position.y, 0);
        }

        

        //using try catch because not every room has all 6 possible doors, but want to check for all possible and avoid errors
        try
        {
            up_door_position = GameObject.Find("Up_Door").transform;
        }
        catch
        {
            //Debug.Log("Up_Door not found");
        }

        try
        {
            down_door_position = GameObject.Find("Down_Door").transform;
        }
        catch
        {
            //Debug.Log("Down_Door not found");
        }

        try
        {
            left_door_position = GameObject.Find("Left_Door").transform;
        }
        catch
        {
            //Debug.Log("Left_Door not found");
        }

        try
        {
            right_door_position = GameObject.Find("Right_Door").transform;
        }
        catch
        {
            //Debug.Log("Right_Door not found");
        }

        try
        {
            out_door_position = GameObject.Find("Out_Door").transform;
        }
        catch
        {
            //Debug.Log("Out_Door not found");
        }

        try
        {
            in_door_position = GameObject.Find("In_Door").transform;
        }
        catch
        {
            //Debug.Log("In_Door not found");
        }
        

    }

    //logic for the layout of the cube maze door connections
    public void load_next_scene(int index_of_door) {
        int current_scene_index = SceneManager.GetActiveScene().buildIndex;
        
        switch (current_scene_index) {

            case 0:

                switch (index_of_door) {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        /*
                        Play_Audio_Diff_Start_Points solution_path_music;
                        solution_path_music.GetComponent<AudioClip>();
                        */
                        SceneManager.LoadScene(9);
                        break;
                    case 2:
                        SceneManager.LoadScene(6);
                        break;
                    case 3:
                        SceneManager.LoadScene(3);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 1:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(7);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        //0_0_1 <-> 0_0_2 Key_BB in 0_2_2
                        bool have_key = false;
                        foreach (var door_key in Static_Vars_1.door_keys)
                        {

                            if (door_key == "Key_BB")
                            {
                                have_key = true;
                            }
                        }
                        if (have_key)
                        {
                            Static_Vars_1.last_saved_room = 2; //save checkpoint, if player dies, will be returned to this room
                            SceneManager.LoadScene(2);
                        }
                        else
                        {
                            //need to change to pop up a dialogue box with message instead of printing to console
                            Debug.Log("You need Key_BB in order to open this door!");
                        }
                        
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 2:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(11);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        //0_0_1 <-> 0_0_2 Key_BB in 0_2_2
                        bool have_key = false;
                        foreach (var door_key in Static_Vars_1.door_keys)
                        {

                            if (door_key == "Key_BB")
                            {
                                have_key = true;
                            }
                        }
                        if (have_key)
                        {
                            SceneManager.LoadScene(1);
                        }
                        else
                        {
                            //need to change to pop up a dialogue box with message instead of printing to console
                            Debug.Log("You need Key_BB in order to open this door!");
                        }
                        
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 3:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(12);
                        break;
                    case 2:
                        SceneManager.LoadScene(0);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 4:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(7);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 5:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(14);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(8);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 6:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(15);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(0);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 7:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(16);
                        break;
                    case 2:
                        SceneManager.LoadScene(4);
                        break;
                    case 3:
                        SceneManager.LoadScene(1);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 8:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(5);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 9:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(0);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(12);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 10:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(19);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(13);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(11);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 11:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(2);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(10);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 12:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(3);
                        break;
                    case 1:
                        SceneManager.LoadScene(21);
                        break;
                    case 2:
                        SceneManager.LoadScene(9);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(14);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 13:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(10);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        bool have_key = false;
                        foreach (var door_key in Static_Vars_1.door_keys)
                        {

                            if (door_key == "Key_CC")
                            {
                                have_key = true;
                            }
                        }
                        if (have_key)
                        {
                            Static_Vars_1.last_saved_room = 14; //save checkpoint, if player dies, will be returned to this room
                            SceneManager.LoadScene(14);
                        }
                        else
                        {
                            //need to change to pop up a dialogue box with message instead of printing to console
                            Debug.Log("You need Key_CC in order to open this door!");
                        }
                        
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 14:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(5);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(17);
                        break;
                    case 4:
                        bool have_key = false;
                        foreach (var door_key in Static_Vars_1.door_keys)
                        {

                            if (door_key == "Key_CC")
                            {
                                have_key = true;
                            }
                        }
                        if (have_key)
                        {
                            SceneManager.LoadScene(13);
                        }
                        else
                        {
                            //need to change to pop up a dialogue box with message instead of printing to console
                            Debug.Log("You need Key_CC in order to open this door!");
                        }
                        
                        break;
                    case 5:
                        SceneManager.LoadScene(12);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 15:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(6);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        //1_2_0 (15) -> 1_2_1 (16) is a locked door that needs Key_AA found in room 2_1_0 (21)
                        bool have_key = false;
                        foreach (var door_key in Static_Vars_1.door_keys) {

                            if (door_key == "Key_AA") {
                                have_key = true;
                            }
                        }
                        if (have_key)
                        {
                            Static_Vars_1.last_saved_room = 16; //save checkpoint, if player dies, will be returned to this room
                            SceneManager.LoadScene(16);
                        }
                        else {
                            //need to change to pop up a dialogue box with message instead of printing to console
                            Debug.Log("You need Key_AA in order to open this door!");
                        }
                        
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 16:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(7);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        bool have_key = false;
                        foreach (var door_key in Static_Vars_1.door_keys)
                        {

                            if (door_key == "Key_AA")
                            {
                                have_key = true;
                            }
                        }
                        if (have_key)
                        {
                            SceneManager.LoadScene(15);
                        }
                        else
                        {
                            //need to change to pop up a dialogue box with message instead of printing to console
                            Debug.Log("You need Key_AA in order to open this door!");
                        }
                        
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 17:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(26);
                        break;
                    case 2:
                        SceneManager.LoadScene(14);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 18:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(24);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        bool have_key = false;
                        foreach (var door_key in Static_Vars_1.door_keys)
                        {

                            if (door_key == "Key_DD")
                            {
                                have_key = true;
                            }
                        }
                        if (have_key)
                        {
                            Static_Vars_1.last_saved_room = 20; //save checkpoint, if player dies, will be returned to this room
                            SceneManager.LoadScene(20);
                        }
                        else
                        {
                            //need to change to pop up a dialogue box with message instead of printing to console
                            Debug.Log("You need Key_DD in order to open this door!");
                        }
                        
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 19:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(10);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 20:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(23);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        bool have_key = false;
                        foreach (var door_key in Static_Vars_1.door_keys)
                        {

                            if (door_key == "Key_DD")
                            {
                                have_key = true;
                            }
                        }
                        if (have_key)
                        {
                            SceneManager.LoadScene(18);
                        }
                        else
                        {
                            //need to change to pop up a dialogue box with message instead of printing to console
                            Debug.Log("You need Key_DD in order to open this door!");
                        }
                        
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 21:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(12);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(22);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 22:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(21);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 23:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(20);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(-1);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 24:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(18);
                        break;
                    case 4:
                        SceneManager.LoadScene(-1);
                        break;
                    case 5:
                        SceneManager.LoadScene(25);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 25:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(-1);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(24);
                        break;
                    case 5:
                        SceneManager.LoadScene(26);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            case 26:
                switch (index_of_door)
                {

                    case 0:
                        SceneManager.LoadScene(17);
                        break;
                    case 1:
                        SceneManager.LoadScene(-1);
                        break;
                    case 2:
                        SceneManager.LoadScene(-1);
                        break;
                    case 3:
                        SceneManager.LoadScene(-1);
                        break;
                    case 4:
                        SceneManager.LoadScene(25);
                        break;
                    case 5:
                        SceneManager.LoadScene(2);
                        break;
                    default:
                        Debug.Log("index of door given is invalid");
                        break;
                }
                break;

            default:
                Debug.Log("Invalid room index #");
                break;
        }
        //save current door statically so that you can access as load_next_scene is called from Player.cs
        coming_out_of_door = index_of_door;
    }
}
