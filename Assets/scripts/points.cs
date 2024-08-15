using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class points : MonoBehaviour
{

     public int pointCounter = 0;
     public TextMeshProUGUI pointText;
     public int totalPoints = 0;


    // Start is called before the first frame update
    void Start()
    {
        pointText.text = pointCounter.ToString(); //set point text
        
    }

    // Update is called once per frame
    void Update()
    {
        pointText.text = pointCounter.ToString(); //update point text 
        
    }
}
