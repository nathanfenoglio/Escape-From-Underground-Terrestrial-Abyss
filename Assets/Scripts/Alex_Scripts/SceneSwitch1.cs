﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch1 : MonoBehaviour
{
      private void OnCollisionEnter2D(Collision2D collision)
      {
          SceneManager.LoadScene(29);
      }
}
