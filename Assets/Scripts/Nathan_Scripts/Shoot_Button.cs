using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!Static_Vars_1.mobile_ui)
        {
            Debug.Log("!Static_Vars_1.mobile_ui");
            gameObject.SetActive(false);
        }
    }

}    
