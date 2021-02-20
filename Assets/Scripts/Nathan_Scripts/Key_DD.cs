using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_DD : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        
        if (player != null)
        {
            //check if player already has key, no need to add to inventory if already in possession
            bool have_key_already = false;
            foreach (string door_key in Static_Vars_1.door_keys)
            {

                if (door_key == "Key_DD")
                {
                    have_key_already = true;
                }
            }

            if (!have_key_already)
            {
                Static_Vars_1.door_keys.Add("Key_DD");
                KeyCounter.countValue += 1;
                //inventory display will need to be updated to show the new key
            }

            Destroy(gameObject);
        }

        //just printing 
        foreach (var key in Static_Vars_1.door_keys)
        {
            Debug.Log(key);
        }
    }
}
