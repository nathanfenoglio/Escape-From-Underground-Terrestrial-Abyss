using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Start()
    {
        GameObject.FindWithTag("Pause Button").GetComponent<Button>().onClick.AddListener(Pause);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (GameIsPaused)
            {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        
        //find all objects of type audio source and unmute, it seems that you need to find all, not just the 1st one to make it work
        //may have to do with the don't destroy on load thing that you did to not have certain songs start every time a scene is changed if the same song
        AudioSource[] audio_arr = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int i = 0; i < audio_arr.Length; i++) {
            audio_arr[i].mute = false;
        }

        GameIsPaused = false; 
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        AudioSource[] audio_arr = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int i = 0; i < audio_arr.Length; i++)
        {
            audio_arr[i].mute = true;
        }

        GameIsPaused = true;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        //LOAD MENU SCENE 
        SceneManager.LoadScene("Title_Screen");
    }

    public void QuitGame() {
        Debug.Log("Quitting game...");
        Application.Quit(); //which will of course work if you build it...right now it does nothing but print Quitting game... 
    }
}
