using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteGameController : MonoBehaviour
{
    //script resobislbe for collecting car parts

    public GameObject jerrycan;  //game objects
    public GameObject jerrycantext;
    public GameObject tire;  
    public GameObject tiretext;
    public GameObject key;  
    public GameObject keytext;


    public bool hasJerry = false; //bool tracks if player has key items
    public bool hasTire = false;
    public bool hasKey = false;



    // Start is called before the first frame update
    void Start()
    {
        //disables text when game is ran so they dont appear at the same time
        jerrycantext.SetActive(false);
        tiretext.SetActive(false);
        keytext.SetActive(false);
        
    }

    public void OnTriggerStay(Collider other) //runs while player is in area
    {

        if(other.gameObject.tag == "jerry") //if player collides with gameobject jerry
        {
            jerrycantext.SetActive(true); //adds pickup text
            if (Input.GetKey(KeyCode.E))  //press e
            {
                jerrycan.gameObject.SetActive(false); //disables jerry object in game
                jerrycantext.SetActive(false); //disables pickup text
                hasJerry = true; //player has item
        
            }
        }

        if(other.gameObject.tag == "tire")  //same as above but with another key item
        {
            tiretext.SetActive(true);
            if (Input.GetKey(KeyCode.E)) 
            {
                tire.SetActive(false);
                hasTire = true;
                tiretext.SetActive(false);
            }
        }

        if(other.gameObject.tag == "key")  //same as above but with another key item
        {
            keytext.SetActive(true);
            if (Input.GetKey(KeyCode.E)) 
            {
                key.SetActive(false);
                hasKey = true;
                keytext.SetActive(false);
            }
        }

    }


    public void OnTriggerExit(Collider other) //disable text when not in area
    {
        jerrycantext.SetActive(false);
        tiretext.SetActive(false);
        keytext.SetActive(false);
    }

}
