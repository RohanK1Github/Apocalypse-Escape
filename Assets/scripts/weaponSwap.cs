using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponSwap : MonoBehaviour
{      

    //SCRIPT NEEDS RE-WORKING (BUT WORKS!)

     public GameObject pistol; //game object for pistol
     public GameObject AR; //game object for AR
     public GameObject GB;
     public pickupController swapPistol; //reference to pickup controller
     public weaponBuy swapAR; //reference to buy weapon script
     public gun isPistolReload; //referece to gun pistol script
     public GunAR isArReload;   //reference to gunAr AR script
     public craftgun swapGB;
     public GameObject chargebar; //disable ui for buildable gun
     public GameObject ammoUI;
     public GunBuildable isGBReload;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            //swap to pistol is "1" is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1) &&  isArReload.isReloading == false && isGBReload.isReloading == false)
        {
            if (swapPistol.hasPistol == true) //only swap to pistol if player has pistol
            {
                pistol.gameObject.SetActive(true); //activate pistol
                AR.gameObject.SetActive(false);  //deactivate ar
                GB.SetActive(false);

            }


        }
                //swap to AR is "2" is pressed
           if (Input.GetKeyDown(KeyCode.Alpha2) && isPistolReload.isReloading == false && isGBReload.isReloading == false) 
            {
                if (swapAR.hasAR == true) //only swap to AR if player has AR
                {
                    AR.gameObject.SetActive(true); //activate ar
                    pistol.gameObject.SetActive(false); //deactivate pistol
                    GB.SetActive(false);           
                }


            }


                      //swap to buildable gun if is "3" is pressed
           if (Input.GetKeyDown(KeyCode.Alpha3) && isPistolReload.isReloading == false &&  isArReload.isReloading == false)
            {
                if (swapGB.hasgun == true) //only swap to AR if player has AR
                {
                    AR.gameObject.SetActive(false); //activate ar
                    pistol.gameObject.SetActive(false); //deactivate pistol
                    GB.SetActive(true);
                     isGBReload.shoot();
                }


            }

    }
}
