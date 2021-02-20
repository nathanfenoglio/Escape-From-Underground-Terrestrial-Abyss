using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCounter : MonoBehaviour
{
    public static int countValue = 0;
    Text KeyCount;
    // Start is called before the first frame update
    void Start()
    {
        KeyCount = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //can change "keys" to something else or leave just the count for keys and add in png image.
        KeyCount.text = "Keys: " + countValue;
    }
    //need to find where collisons occur with keys to implement the count when the collison occurs.
    //Incrementing by 1.
    //KeyCounter.countValue += 1;
}