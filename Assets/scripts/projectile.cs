using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float lifetime = 0.1f;
 

    void Start()
    {
        Destroy(gameObject, lifetime);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
