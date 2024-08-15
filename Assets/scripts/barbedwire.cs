using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barbedwire : MonoBehaviour
{

    public saw s;
    public movementController player;

    public GameObject wire;
    public GameObject wireText;
    public GameObject missingSawText;
    public AudioSource sawnoise;

    //barbed wire door class
    //responsible for blocking path until player has cut the barbed wire
    //barbed wire damages player if touched
    //player can cut barbed wire if they have specific item

    // Start is called before the first frame update
    void Start()
    {
        wireText.SetActive(false); //disabled text to start with
        missingSawText.SetActive(false);
    }


    public void OnTriggerEnter(Collider other) //runs while player enters area
    {

        if(other.gameObject.tag == "Player") //if player collides with barbed wire
        {
            player.Playerhealth -= 4; //take damage
        }

    }


    public void OnTriggerStay(Collider other) //runs while player is in area
    {
        if(other.gameObject.tag == "Player") //if player collides with gameobject saw
        {

            if(s.hasSaw == false)
            {
                missingSawText.SetActive(true); //text displays when walk into barbed wire - tells player they are missing item
            }
            else
            {
                wireText.SetActive(true); //tells player to cut barbed wire with item if they aleady have it
            }


            if (Input.GetKey(KeyCode.E)) //press e to cut wire
            {
                if(s.hasSaw == true)
                {
                    sawnoise.Play(); //cutting sound
                    wireText.SetActive(false); //disables text
                    wire.SetActive(false); //disables text
                }
            }
     
        }
    }


    public void OnTriggerExit(Collider other) //runs when player leaves area
    {
        wireText.SetActive(false); //disables text
        missingSawText.SetActive(false);
    
    }

}
