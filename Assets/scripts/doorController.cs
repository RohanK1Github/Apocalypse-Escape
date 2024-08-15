using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    //script responsible for opening door - player must use points to open door

    public points p; 
    public GameObject door1;
    public GameObject door2;
    public GameObject openDoor1;
    public GameObject openDoor2;

    public GameObject doorText;
    public AudioSource doorNoise;
    
    // Start is called before the first frame update
    void Start()
    {
        doorText.SetActive(false); //disable text on start up
    }


    public void OnTriggerStay(Collider other) //runs while player is in area
    {
        if(other.gameObject.tag == "Player") //if player collides with door
        {
            doorText.SetActive(true); 
            if (Input.GetKey(KeyCode.E) && p.pointCounter >= 1500)  //press e and use points to open door
            {
                doorText.SetActive(false);
                p.pointCounter -= 1500;
                door1.SetActive(false);
                door2.SetActive(false);
                openDoor1.SetActive(true);
                openDoor2.SetActive(true);
                doorNoise.Play();
            }
        }
    }


    public void OnTriggerExit(Collider other) //runs while player is in area
    {
        doorText.SetActive(false);
    }

}
