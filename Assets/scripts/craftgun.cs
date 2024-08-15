using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftgun : MonoBehaviour
{
    //script responsible for collecting and crafting the buildable gun. //gun3partscontroller class used to collect parts

    public Gun3PartsController g;

    public GameObject craftingBench;    //game objects
    public GameObject missingPartsText;
    public GameObject buildText;
    public GameObject pickuptext;
    public GameObject pistol; 
    public GameObject ar;
    public GameObject newGun;
    public GameObject bar;
    public GameObject ammoUI;
    public GameObject tableGun;

    public GameObject scope;    //gameobject gun parts needed for building
    public GameObject mag;
    public GameObject barrel;


    public bool hasgun = false; //tracks if player has new gun
    public bool gunBuilt = false; //tracks if its been built


    // Start is called before the first frame update
    void Start()
    {
        missingPartsText.SetActive(false); //disables text on start up
        buildText.SetActive(false);
        pickuptext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (gunBuilt == true) //if gun has been built
        {
            pickuptext.SetActive(true);
            if (Input.GetKey(KeyCode.E)) //pick up gun
                {
                    tableGun.SetActive(false);
                    Destroy(pickuptext);
                    hasgun = true;

                    pistol.SetActive(false); //when new gun is picked up disable other guns
                    ar.SetActive(false);
                    newGun.SetActive(true);
                    bar.SetActive(true);
                }
            }    
    }

       public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

            if(g.hasAllParts == true)
            {
                buildText.SetActive(true);
                if (Input.GetKey(KeyCode.E)) //build gun
                {
                    Destroy(buildText); 
                    buildText.SetActive(false);
                    scope.SetActive(true);
                    mag.SetActive(true);
                    barrel.SetActive(true);
                    StartCoroutine(wait()); //wait a second to prevent picking up and building to happen at the same time
                }
            }
            else
            {
                missingPartsText.SetActive(true);
            }
        }
    }

        IEnumerator wait() //wait a second
    {
        yield return new WaitForSeconds(1); 
        gunBuilt = true;
    }


    public void OnTriggerExit(Collider other) //disable text when leaving the area
    {
        missingPartsText.SetActive(false);
        buildText.SetActive(false);
        pickuptext.SetActive(false);
       
    }





}
