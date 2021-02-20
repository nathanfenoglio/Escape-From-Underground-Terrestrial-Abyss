using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save //class for the purpose of storing saved data
{
    //variable to store levels completed or current level player is on (can assume other levels have been completed  if this is sequential
    
    public float player_health;
    public float playerPosX;
    public float playerPosY;

    //public List<Items> Inventory = new List<Items>();
    //variables for non-default abilities player has
    public bool canSprayShot;
    public bool canDoubleJump;
    public bool canRegShot; //regular shot
    public bool halfDmg; //half damage

    //bag
    public ArrayList keys = new ArrayList();

    //checkpoint
    public int lastSavedRoom;
}
