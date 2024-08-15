using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupController : MonoBehaviour
{   
    public bool hasGun = false;
    public GameObject item;  //references to game objects
    public GameObject itemText;
    public GameObject door;
    public GameObject brokendoor;
    public waveController WC; //refernce to wave script
    public bool hasPistol = false;


    // Start is called before the first frame update
    void Start()
    {
        item.SetActive(false);
        itemText.SetActive(false);
        
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            itemText.SetActive(true);
            if (Input.GetKey(KeyCode.E)) //pick up gun
            {
                this.gameObject.SetActive(false);
                item.gameObject.SetActive(true);
               //hasGun = true;
                WC.StartNextWave(); //start next wave - called from wave script
                itemText.SetActive(false);
                door.SetActive(false); //open door in spawn house
                hasPistol = true;
                Debug.Log("start initial wave");
                

            }
        }
    }

    public void OnTriggerExit(Collider other) //disable text when not in area
    {
        itemText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
