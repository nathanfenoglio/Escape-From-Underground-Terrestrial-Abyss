using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//for Xml functionality
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

public class XMLSaveLoad : MonoBehaviour
{
    //actually should be in main gameplay loop i believe?
    //put in save data to save object
    private Save createSave()
    {
        Save save = new Save();

        //getting player current health - not sure if this was the right way to grab the info
        save.player_health = Static_Vars_1.player_health_static;
        GameObject player = GameObject.Find("Player");

        //getting player current positions
        save.playerPosX = player.transform.position.x;
        save.playerPosY = player.transform.position.y;

        //non-default player abilities
        save.canSprayShot = Static_Vars_1.spray_shot_static;
        save.canDoubleJump = Static_Vars_1.double_jump_static;
        save.canRegShot = Static_Vars_1.regular_shot;
        save.halfDmg = Static_Vars_1.half_damage_static;

        //key counter on-screen
        


        //inventory
        for (int i = 0; i < Static_Vars_1.door_keys.Count; ++i)
        {
            save.keys.Add(Static_Vars_1.door_keys[i]);
        }

        //last checkpoint scene
        save.lastSavedRoom = Static_Vars_1.last_saved_room;

        return save;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Static_Vars_1.last_saved_room = SceneManager.GetActiveScene().buildIndex;
            XMLSave();
        }
    }


    //writing the XML
    private void XMLSave()
    {
        Save save = createSave();
        XmlDocument xml = new XmlDocument();

        //Create XMLDocument Elements
        XmlElement parent = xml.CreateElement("Save"); //<Save> and then we have the rest of our <element(s)></element> here </Save>

        //health
        XmlElement healthEle = xml.CreateElement("Health"); //<Save><Health></Health></Save> -- tag name: Health
        healthEle.InnerText = save.player_health.ToString();
        parent.AppendChild(healthEle);

        //positions
        XmlElement posXEle = xml.CreateElement("PlayerPositionX");
        posXEle.InnerText = save.playerPosX.ToString();
        parent.AppendChild(posXEle);

        XmlElement posYEle = xml.CreateElement("PlayerPositionY");
        posYEle.InnerText = save.playerPosY.ToString();
        parent.AppendChild(posYEle);

        //non-default abilities
        XmlElement sprayShotEle = xml.CreateElement("SprayShot");
        sprayShotEle.InnerText = save.canSprayShot.ToString();
        parent.AppendChild(sprayShotEle);

        XmlElement doubleJumpEle = xml.CreateElement("DoubleJump");
        doubleJumpEle.InnerText = save.canDoubleJump.ToString();
        parent.AppendChild(doubleJumpEle);

        XmlElement regShotEle = xml.CreateElement("RegularShot");
        regShotEle.InnerText = save.canRegShot.ToString();
        parent.AppendChild(regShotEle);

        XmlElement halfDmgEle = xml.CreateElement("HalfDamage");
        halfDmgEle.InnerText = save.halfDmg.ToString();
        parent.AppendChild(halfDmgEle);

        //inventory
        XmlElement keyEle;
        for (int i = 0; i < save.keys.Count; ++i)
        {
            keyEle = xml.CreateElement("Key");
            keyEle.InnerText = save.keys[i].ToString();
            parent.AppendChild(keyEle);
        }

        //checkpoint scene
        XmlElement lastSavedRoomEle = xml.CreateElement("LastSavedRoom");
        lastSavedRoomEle.InnerText = save.lastSavedRoom.ToString();
        parent.AppendChild(lastSavedRoomEle);


        //write out to a  file
        xml.AppendChild(parent); //appaending all elements to xml doc
        xml.Save(Application.dataPath + "/XMLData.text");
        if (File.Exists(Application.dataPath + "/XMLData.text")) //overwriting
        {
            Debug.Log("XML File Saved!");
        }
    }

    //load the game
    public void XMLLoad()
    {
        if (File.Exists(Application.dataPath + "/XMLData.text"))
        {
            Save save = new Save();
            XmlDocument xml = new XmlDocument();
            xml.Load(Application.dataPath + "/XMLData.text");

            //get save file data

            //health
            XmlNodeList health = xml.GetElementsByTagName("Health");
            save.player_health = float.Parse(health[0].InnerText);

            //position
            XmlNodeList posX = xml.GetElementsByTagName("PlayerPositionX");
            save.playerPosX = float.Parse(posX[0].InnerText);
            XmlNodeList posY = xml.GetElementsByTagName("PlayerPositionY");
            save.playerPosY = float.Parse(posY[0].InnerText);

            //non-default abilities
            XmlNodeList sprayShot = xml.GetElementsByTagName("SprayShot");
            save.canSprayShot = bool.Parse(sprayShot[0].InnerText);
            XmlNodeList doubleJump = xml.GetElementsByTagName("DoubleJump");
            save.canDoubleJump = bool.Parse(doubleJump[0].InnerText);
            XmlNodeList regShot = xml.GetElementsByTagName("RegularShot");
            save.canRegShot = bool.Parse(regShot[0].InnerText);
            XmlNodeList halfDmg = xml.GetElementsByTagName("HalfDamage");
            save.halfDmg = bool.Parse(halfDmg[0].InnerText);

            //inventory
            XmlNodeList key = xml.GetElementsByTagName("Key");
            for (int i = 0; i < key.Count; ++i)
            {
                save.keys.Add(key[i].InnerText);
            }

            //checkpoint scene index
            XmlNodeList lastSavedScene = xml.GetElementsByTagName("LastSavedRoom");
            save.lastSavedRoom = int.Parse(lastSavedScene[0].InnerText);
            /*
            Static_Vars_1.last_saved_room = save.lastSavedRoom;
            AsyncOperation loadLevelAsync = SceneManager.LoadSceneAsync(Static_Vars_1.last_saved_room); //load in scene

            //assign save data to current game status
            if(loadLevelAsync.isDone)
            {
                Debug.Log("Hey I reached inside loadLevelAsync.isDone");
                Static_Vars_1.player_health_static = save.player_health;
                GameObject player = GameObject.Find("Player");
                player.transform.position = new Vector3(save.playerPosX, save.playerPosY, 0);
                Static_Vars_1.spray_shot_static = save.canSprayShot;
                Static_Vars_1.double_jump_static = save.canDoubleJump;
                Static_Vars_1.regular_shot = save.canRegShot;
                Static_Vars_1.half_damage_static = save.halfDmg;
                for (int i = 0; i < save.keys.Count; ++i)
                {
                    Static_Vars_1.door_keys.Add(save.keys[i]);
                }
            }
            */

            //NATHAN
            //moved the load scene stuff to after all of the other variables have been loaded
            /*
            Static_Vars_1.last_saved_room = save.lastSavedRoom;
            SceneManager.LoadScene(Static_Vars_1.last_saved_room); //load in scene
            */
            //assign save data to current game status
            Static_Vars_1.player_health_static = save.player_health;
            //NATHAN
            //commenting out the player transform stuff because giving a null ref exception, think because Player is not in the Title Screen
            //but would need to wait I think until after scene is loaded to place the player and have reference to the intended player object...
            /*
            GameObject player = GameObject.Find("Player");
            player.transform.position = new Vector3(save.playerPosX, save.playerPosY, 0);
            */
            Static_Vars_1.player_position_x = save.playerPosX;
            Static_Vars_1.player_position_y = save.playerPosY;
            Static_Vars_1.spray_shot_static = save.canSprayShot;
            Static_Vars_1.double_jump_static = save.canDoubleJump;
            Static_Vars_1.regular_shot = save.canRegShot;
            Static_Vars_1.half_damage_static = save.halfDmg;
            for (int i = 0; i < save.keys.Count; ++i)
            {
                Static_Vars_1.door_keys.Add(save.keys[i]);
            }

            //NATHAN
            //setting static variable in Static_Vars_1 script for is_loading to check in player function and set position
            Static_Vars_1.is_loading = true;
            //moved the load scene here 
            Static_Vars_1.last_saved_room = save.lastSavedRoom;
            SceneManager.LoadScene(Static_Vars_1.last_saved_room); //load in scene
        }
        else
        {
            Debug.Log("File not found!");
        }
    }
}
