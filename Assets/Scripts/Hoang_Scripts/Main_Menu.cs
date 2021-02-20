using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.Xml; //provide xml
using System.Security.Cryptography;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;

public class Main_Menu : MonoBehaviour
{   
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //load next scene in the queue
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelManager");
    }
}
