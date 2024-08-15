using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmoDrop : MonoBehaviour
{
    //reponsible giving player ammo from dropped ammo boxes from enemy

    public gun pistol; //reference to ar script
    public GameObject item; 


    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            pistol.reserveAmmo += 3;
            Destroy(gameObject);

        }



    }





}
