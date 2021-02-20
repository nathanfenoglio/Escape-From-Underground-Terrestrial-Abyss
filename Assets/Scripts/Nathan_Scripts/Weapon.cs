using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Transform firepoint_barrel;
    //adding other barrels for spray shots
    public Transform firepoint_barrel_up_10;
    public Transform firepoint_barrel_down_10;


    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;

    public Player player_ref;

    void Start()
    {
        firepoint_barrel_up_10 = transform.Find("GunBarrel10DegUp");
        firepoint_barrel_down_10 = transform.Find("GunBarrel10DegDown");
    }

    // Update is called once per frame
    void Update()
    {
        //check for mobile_ui otherwise Fire1 is registered everytime mouse is clicked
        if (!Static_Vars_1.mobile_ui && Input.GetButtonDown("Fire1") && (Static_Vars_1.regular_shot || Static_Vars_1.spray_shot_static))
        {
            Shoot();
        } 

    }

    public void Shoot() {

        if (Static_Vars_1.spray_shot_static)
        {
            if (player_ref.GetComponent<Player>().is_crouching)
            {
                //move barrel positions lower
                firepoint_barrel.position = new Vector3(firepoint_barrel.position.x, firepoint_barrel.position.y - 0.3f, 0);
                firepoint_barrel_up_10.position = new Vector3(firepoint_barrel_up_10.position.x, firepoint_barrel_up_10.position.y - 0.3f, 0);
                firepoint_barrel_down_10.position = new Vector3(firepoint_barrel_down_10.position.x, firepoint_barrel_down_10.position.y - 0.3f, 0);

                //straight ahead
                Instantiate(bulletPrefab, firepoint_barrel.position, firepoint_barrel.rotation);

                //10 degree angles
                Instantiate(bulletPrefab2, firepoint_barrel_up_10.position, firepoint_barrel_up_10.rotation);
                Instantiate(bulletPrefab3, firepoint_barrel_down_10.position, firepoint_barrel_down_10.rotation);

                //reset all barrel positions after bullet has been instantiated
                firepoint_barrel.position = new Vector3(firepoint_barrel.position.x, firepoint_barrel.position.y + 0.3f, 0);
                firepoint_barrel_up_10.position = new Vector3(firepoint_barrel_up_10.position.x, firepoint_barrel_up_10.position.y + 0.3f, 0);
                firepoint_barrel_down_10.position = new Vector3(firepoint_barrel_down_10.position.x, firepoint_barrel_down_10.position.y + 0.3f, 0);
            }
            else {
                //straight ahead
                Instantiate(bulletPrefab, firepoint_barrel.position, firepoint_barrel.rotation);

                //10 degree angles
                Instantiate(bulletPrefab2, firepoint_barrel_up_10.position, firepoint_barrel_up_10.rotation);
                Instantiate(bulletPrefab3, firepoint_barrel_down_10.position, firepoint_barrel_down_10.rotation);
            }
            
        }
        //check if player has regular_shot power up 
        else if(Static_Vars_1.regular_shot)
        {

            if(player_ref.GetComponent<Player>().is_crouching)
            { //if crouching 
                firepoint_barrel.position = new Vector3(firepoint_barrel.position.x, firepoint_barrel.position.y - 0.3f, 0);
                Instantiate(bulletPrefab, firepoint_barrel.position, firepoint_barrel.rotation);
                firepoint_barrel.position = new Vector3(firepoint_barrel.position.x, firepoint_barrel.position.y + 0.3f, 0);

            }
            else {
                Instantiate(bulletPrefab, firepoint_barrel.position, firepoint_barrel.rotation);
            }
            
        }

    }

}
