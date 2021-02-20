using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




//TRANSITION BETWEEN LEVELS


public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    //turning this into a variable so we can adjust the animation wait time.
    public float transitionTime = 1f;
    //public Level_Controller_1 level_controller_1;

    // Update is called once per frame
    void Update()
    {
        //when the mouse button is clicked, it will load in the next scene.

        //change this to whenever we reach the door instead. ********
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Loading the next level
            LoadNextLevel();

        }
    }


    public void LoadNextLevel()
    {
        //This loads in the next level only.
        //Includes transition animation.
        //Need to make sure it can load other levels as well.
        //level_controller_1 = GameObject.FindObjectOfType(typeof(Level_Controller_1)) as Level_Controller_1;


        //loading in index of door. Not working atm.
        StartCoroutine(LoadLevel(//SceneManager.LoadScene(Level_Controller_1.load_next_scene(index_of_door)))); 
        SceneManager.GetActiveScene().buildIndex + 1));


    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //plays animation starting from the beginning of Start trigger to the end point.
        transition.SetTrigger("Start");

        //waits
        yield return new WaitForSeconds(transitionTime);

        //loads scene
        SceneManager.LoadScene(levelIndex);


    }
}
