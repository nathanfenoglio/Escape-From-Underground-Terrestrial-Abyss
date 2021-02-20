using UnityEngine;
using System.Collections;

 public class respawn2 : MonoBehaviour {
     public float threshold;


     void FixedUpdate () {
         if (transform.position.y < threshold)
             transform.position = new Vector3(-29, -6, 0);
     }
 }
