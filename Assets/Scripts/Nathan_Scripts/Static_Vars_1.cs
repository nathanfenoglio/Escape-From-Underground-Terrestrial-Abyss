using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System.Collections;

public class Static_Vars_1 : MonoBehaviour
{
    //start scene
    public static bool start_game = true;//true;

    //powerups
    public static bool regular_shot = false;
    public static bool spray_shot_static = false;
    public static bool double_jump_static = false;
    public static bool half_damage_static = false;

    //zero gravity
    public static bool zero_gravity = false;
    
    //persistent health of player
    public static float player_health_static = 1.0f;

    //array to store the string names of the keys that have been acquired for checking if a door can be traveled through
    public static ArrayList door_keys = new ArrayList();

    //store last savepoint room that the player will return to after dying
    public static int last_saved_room;

    //final bubble boy boss tokens (need 4)
    public static int bubble_boy_kills = 0;

    //mobile input or not
    public static bool mobile_ui = true;

    //specify if loading a saved scene
    public static bool is_loading = false;

    //player coordinates in the level of last save spot to return player to 
    public static float player_position_x = 0f;
    public static float player_position_y = 0f;

}
