using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgradeController : MonoBehaviour
{

    //script responivle for health upgrade item. buys health upgrade with points

    public movementController upgradehealth; //reference to player script (movementController)
    public points p; //reference to point script
    public GameObject item; 
    public GameObject itemText;


    // Start is called before the first frame update
    void Start()
    {
        itemText.SetActive(false);
    }


     public void OnTriggerStay(Collider other) //run when player enters area
    {
        if(other.gameObject.tag == "Player")
        {
            itemText.SetActive(true); //activate buy text when player is near
            
            if (Input.GetKey(KeyCode.E))
            {
                if(p.pointCounter >= 2500)
                {
                    p.pointCounter -= 2500; 
                    upgradehealth.maxHealth = 50;
                    itemText.SetActive(false); 
                    item.gameObject.SetActive(false);
                }
            }
        }
    }

    public void OnTriggerExit(Collider other) //deactivate buy text when player leaves area
    {
    itemText.SetActive(false); 
    }

}
