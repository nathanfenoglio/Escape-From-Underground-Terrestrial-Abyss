using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    /*Start is called before the first frame update*/
    void Start()
    {

    }

    /*Update is called once per frame*/
    void Update()
    {

    }


    /*
     * TO TEST IF BUTTON WORKS ONLY.
     * Change in inspector of button to use Pass() instead to load the actual scenes.
    */
    public void ButtonInteract()
    {
        //load in the respective scene for the level here.

        Debug.Log("Testing. Button was clicked");
    }

    //Add this to scenes for when player passes the level to unlock next level on the level screen.
    public void Pass()
    {
        int Current_Level = SceneManager.GetActiveScene().buildIndex;

        if(Current_Level >= PlayerPrefs.GetInt("Level_Unlocked"))
        {
            PlayerPrefs.SetInt("Level_Unlocked", Current_Level + 1);
        }

        //Text here is to test if the correct level was unlocked
        Debug.Log("LEVEL"+PlayerPrefs.GetInt("Level_Unlocked")+ " UNLOCKED");
    }
}
