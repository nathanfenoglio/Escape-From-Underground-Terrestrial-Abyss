﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform_ChildPlatform_Horizontal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);
    }
}
