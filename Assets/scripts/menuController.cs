using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class menuController : MonoBehaviour
{
    [SerializeField] public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    public AudioMixer mixer; 
    public string parameterName = "MasterVolume"; //master volume 

    public Slider volSlider;
    public Toggle fullToggle;
    // public Slider SensSlider;

    public movementController sensitivity;
   

    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //switch to next scene (the game)

    }

    public void QuitButton() //called in engine on exit button object
    {
        Application.Quit(); //exit application. Ran when exit button is pressed
    }


    public void ResultionDropDown()
    {
        resolutions = Screen.resolutions; //all available resolutions

        resolutionDropdown.ClearOptions(); // clear default options
        
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height +" "+ resolutions[i].refreshRate+" Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        if(currentResolutionIndex >= 0 && currentResolutionIndex < resolutions.Length)
        {
            SetResolution(currentResolutionIndex);
        }

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex); //save resolution so its consistent between scenes
        //done for volume and toggle fullscreen aswell                                                            
    }




    public void ToggleFullscreen() //called in engine on toggle button
    {
    Screen.fullScreen = !Screen.fullScreen;  //toggle the fullscreen mode
    PlayerPrefs.SetInt("Fullscreen",Screen.fullScreen ? 1 : 0 ); //save option between scenes
    }


    public void SetVolume(float volume)
    {
        if (volume <= 0.0001f)
    {
        volume = 0.0001f;
    }

        float volumeDb = Mathf.Log10(volume) * 20;
        mixer.SetFloat(parameterName, volumeDb); 
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }


    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        SetVolume(savedVolume); 
        volSlider.value = savedVolume;
        volSlider.onValueChanged.AddListener(SetVolume);
        ResultionDropDown();

    }


}
