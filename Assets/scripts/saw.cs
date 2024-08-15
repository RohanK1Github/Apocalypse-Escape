using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saw : MonoBehaviour
{

    public points p; //reference to point script
    public GameObject Saw;
    public GameObject sawtext;
    public bool hasSaw = false;
    

    // Start is called before the first frame update
    void Start()
    {
        sawtext.SetActive(false);
        
    }

    public void OnTriggerStay(Collider other) //runs while player is in area
    {
        if(other.gameObject.tag == "Player")
        {
            sawtext.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                if(p.pointCounter >= 500)
                {
                    p.pointCounter -= 500;
                    hasSaw = true;
                    Saw.SetActive(false);
                    sawtext.SetActive(false);
                }
            }  //press e
        }



    }

     public void OnTriggerExit(Collider other) //runs while player is in area
    {
        sawtext.SetActive(false);
    }


}
