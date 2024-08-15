using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaUpgradeController : MonoBehaviour
{

    public movementController upgradeStamina;
    public points p; //reference to point script
    public GameObject item; 
    public GameObject itemText;

    // Start is called before the first frame update
    void Start()
    {
         itemText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    upgradeStamina.maxStamina = upgradeStamina.maxStamina + 50;
                    upgradeStamina.staminaVal = upgradeStamina.staminaVal + 50;
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

    public void BuyUpgradeHealth()
    {




    }


}
