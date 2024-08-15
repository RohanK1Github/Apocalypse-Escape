using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponBuy : MonoBehaviour
{

    public int weaponCost; //point cost of weapon
    public points p; //reference to point script
    public GameObject item; 
    public GameObject itemText;
    public bool hasAR = false;
    public GameObject pistol;


    // Start is called before the first frame update
    void Start()
    {
        item.SetActive(false);
        itemText.SetActive(false);
    }

        public void OnTriggerStay(Collider other) //run when player enters area
    {
        if(other.gameObject.tag == "Player")
        {
            itemText.SetActive(true); //activate buy text when player is near
            if (Input.GetKey(KeyCode.E)) //if press "e"
            {
                if(p.pointCounter >= 1000) //if player has 1000 points
                {
                    p.pointCounter -= 1000;             //buy weapon
                    this.gameObject.SetActive(false); //set current item to false 
                    item.gameObject.SetActive(true);    //set current item to true (swap to purchased weapon)
                    itemText.SetActive(false);
                    hasAR = true;
                    pistol.gameObject.SetActive(false);
                }

            }
        }
    }


       public void OnTriggerExit(Collider other) //deactivate buy text when player leaves area
    {
        itemText.SetActive(false); 
    }

    void Update()
    {
        
    }
}
