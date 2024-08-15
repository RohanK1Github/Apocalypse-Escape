using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    //script responsible for car parts. lets player collect missing parts and add them to the car

    public CompleteGameController cg; //refrence to script
    public GameObject MissingItemsText; //gameobjects
    public GameObject AddPartText;
    public GameObject EscapeText;
    public GameObject Player;
    public GameObject tire;
    public GameObject EscapeCanvas;
    public GameObject gameCanvas;
    public GameObject time;

    public AudioSource fuelsound;   //audio sources
    public AudioSource clank;
    public AudioSource ignition;
    public AudioSource engine;

    public bool allParts = false;

    public bool inarea = false;

    public Camera carCam; //car view
    
    
    // Start is called before the first frame update
    void Start()                //sets all text to false when game starts. so they dont all appear at once
    {
        MissingItemsText.SetActive(false);
        AddPartText.SetActive(false);
        EscapeText.SetActive(false);
        carCam.gameObject.SetActive(false); //sets car cam to false

    }

    // Update is called once per frame
    void Update()
    {

        if(allParts == true) //if player has all parts needed to escape
        {

                if (Input.GetKeyDown(KeyCode.E)) //press e on car
                {
                    //escape
                    EscapeText.SetActive(false); //disable text
                    Camera.main.gameObject.SetActive(false); //disable main cam
                    carCam.gameObject.SetActive(true); //enable cam view
                    Player.SetActive(false); //disables player 
                    EscapeCanvas.SetActive(true); //enable new ui
                    gameCanvas.SetActive(false); //disable old ui
                    ignition.Play(); //play audio
                    engine.Play();
                    Cursor.lockState = CursorLockMode.None; //adds curser back
                }
        }
    }


     public void OnTriggerStay(Collider other) //runs while player is in area
    {

        if(other.gameObject.tag == "Player") //if player collides with car
        {

            inarea = true; //tracks if player is in area

            if(cg.hasJerry == false || cg.hasTire == false || cg.hasKey == false) //if player does not have atleast 1 of the parts from completegame controller
            {
                MissingItemsText.SetActive(true); //missing text is true
            }

            if(cg.hasJerry == true && cg.hasTire == true && cg.hasKey == true) //if player has all items from complete game controller 
            {
                AddPartText.SetActive(true); //add parts text enabled

                if (Input.GetKey(KeyCode.E))    //press e to add parts
                {
                    Destroy(AddPartText); //removes text
                    AddPartText.SetActive(false); 
                    tire.SetActive(true); //adds missing tire to car
                    fuelsound.Play(); //plays fueling sound
                    clank.Play(); //plays clanking sound
                    Destroy(time,2); 
                    allParts = true; //all parts true
                }
            }
        }
    }

      public void OnTriggerEnter(Collider other) //runs when player enters area
    {
           if(allParts == true) 
            {
                EscapeText.SetActive(true); //text is added
            }
    }



    public void OnTriggerExit(Collider other) //disable text when left area
    {
        EscapeText.SetActive(false);
        MissingItemsText.SetActive(false);
        AddPartText.SetActive(false);
        
        inarea = false; //tracks when player leaves area
    }

}
