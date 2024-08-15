using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseSensitivity : MonoBehaviour
{

    public Slider SensSlider; //slider object
    public movementController sens; //refrences to where sensitivity is set
    public float savedSensitivity; //save sens - used between scenes

    public void SetSensitivity() //called in engine on slider object
    {
        sens.mouseSensitivity = SensSlider.value; //sensitivity is equal to what slider is set to 
        PlayerPrefs.SetFloat("MouseSensitivity", SensSlider.value); //value is saved
    }

    // Start is called before the first frame update
    void Start()
    {
        savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 500f);
        SensSlider.value = savedSensitivity; //load value on slider
        sens.mouseSensitivity = savedSensitivity; //load saved sense
    }

}
