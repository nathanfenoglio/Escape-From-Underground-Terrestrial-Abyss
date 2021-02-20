using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int Level_Unlocked;
    //Array of all buttons we will use
    public Button[] buttons;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs returns the value corresponding to key in the preference file (if it exists)
        //parameters: (string key, int default value)
        //Level_Unlocked = PlayerPrefs.GetInt("Level_Unlocked", 1);

        /*
         We want all of the buttons at first to be non-interactive at first,
         until the level has been passed.
        */
        //commenting out to get rid of error, still working
        /*
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
        */
        /*
         Then, we want to loop through the amount of levels that are unlocked,
         and change those to true.
        */
        /*for(int i = 0; i < Level_Unlocked; i++)
        {
            buttons[i].interactable = true;
        }*/
    }

    /*
     * Creating a LoadLevel function that loads in the level of the index
     */
    public void LoadLevel(int level_index)
    {
        SceneManager.LoadScene(level_index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
