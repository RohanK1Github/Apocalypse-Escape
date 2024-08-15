using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ammoBuyController : MonoBehaviour
{
    public int cost; //point cost of weapon
    public points p; //reference to point script
    public GameObject item; 
    public GameObject itemText;
    public GunAR ar; //reference to gun script
    public gun pistol; //reference to ar script
    public int arAmmoCost = 800;
    public int pistolAmmoCost = 400;
    public bool playInArea = false; //tracks when player is close enought to game object to buy
    public bool isAR = false; //is holding pistol or Ar
    public bool isPistol = false;
    public bool pistolBox; //value assigned in editor. checks if ammo box is an ar ammo box or pistol ammo box
    public bool arBox;

    // Start is called before the first frame update
    void Start()
    {
        itemText.SetActive(false); //text object disabled
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playInArea == true && Input.GetKeyDown(KeyCode.E) && isAR == true && arBox == true) //buy ar ammo
        {
              if(p.pointCounter >= arAmmoCost) //if player has 1000 points
                {
                    p.pointCounter -= arAmmoCost;             //buy weapon
                    ar.reserveAmmoAR += 60;
                    
                }
        }

        if (playInArea == true && Input.GetKeyDown(KeyCode.E) && isPistol == true && pistolBox == true) //buy pistol ammo
        {
                if(p.pointCounter >= pistolAmmoCost) //if player has 1000 points
                {
                    p.pointCounter -= pistolAmmoCost;             //buy weapon
                    pistol.reserveAmmo += 60;
                }
        }

    }


        public void OnTriggerStay(Collider other) //run when player enters area
    {
        if(other.gameObject.tag == "ar")
        {
            itemText.SetActive(true); //activate buy text when player is near
            playInArea = true;
            isAR = true;
  
        }

        if(other.gameObject.tag == "pistol")
        {
            itemText.SetActive(true); //activate buy text when player is near
            playInArea = true;
            isPistol = true;
        }
    }

    public void OnTriggerExit(Collider other) //deactivate buy text when player leaves area
    {
        itemText.SetActive(false); 
        playInArea = false;
        isPistol = false;
        isAR = false;
    }

}
