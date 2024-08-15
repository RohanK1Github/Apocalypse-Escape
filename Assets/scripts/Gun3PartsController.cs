using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun3PartsController : MonoBehaviour
{
    //gun3partscontroller class used to collect parts for buildable gun. craftgun class responsiblr for 
    //building gun after parts collected

    public bool hasAllParts = false; //has parts
    public bool hasPart1 = false;
    public bool hasPart2 = false;
    public bool hasPart3 = false;

    public GameObject pickUpText; //different parts
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;


    // Start is called before the first frame update
    void Start()
    {
        pickUpText.SetActive(false); //disable text on start up
        
    }

    public void OnTriggerStay(Collider other) //check if collison with parts
    {
        if(other.gameObject.tag == "gunscope") //collison with part
        {
            pickUpText.SetActive(true); //enable pick up text

            if (Input.GetKey(KeyCode.E)) //pick up part
            {
                hasPart1 = true; //tracks that part is collected
                hasparts(); //runs has parts
                pickUpText.SetActive(false); //disables text after pick up
                part1.SetActive(false); //disables game object after pick up
            }

        }

           if(other.gameObject.tag == "gunmag") //same as above but with other part
        {
            pickUpText.SetActive(true);

            if (Input.GetKey(KeyCode.E)) 
            {
                hasPart2 = true;
                hasparts();
                pickUpText.SetActive(false);
                part2.SetActive(false);
            }
        }

           if(other.gameObject.tag == "gunbarrel") //same as above but with other part
        {
            pickUpText.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                hasPart3 = true;
                hasparts();
                pickUpText.SetActive(false);
                part3.SetActive(false);
            }
        }
    }


      public void OnTriggerExit(Collider other) //disables all text afer leaving area
    {
        pickUpText.SetActive(false);
       
    }



    void hasparts() //tracks if player has all parts
    {
        if(hasPart1 == true && hasPart2 == true && hasPart3 == true)
        {
            hasAllParts = true; //sets true
        }

    }

}
