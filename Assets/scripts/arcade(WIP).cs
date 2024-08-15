using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcade : MonoBehaviour
{

    public GameObject itemText;


    // Start is called before the first frame update
    void Start()
    {
        
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
            

  
        }

    }

    public void OnTriggerExit(Collider other) //deactivate buy text when player leaves area
    {
        itemText.SetActive(false); 
       
    }






}
