using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixed_Joystick_Active : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!Static_Vars_1.mobile_ui)
        {
            gameObject.SetActive(false);
        }
        else {
            gameObject.SetActive(true);
        }
    }

}
