using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements.Experimental;


public class MenuControl : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private GameObject confirmationPrompt = null;
    [SerializeField] private float defaultVolume = 5.0f;


    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text ControllerSenTextValue = null;
    [SerializeField] private Slider ControllerSenSlider = null;
    [SerializeField] private int defaultControllerSen = 5;
    public int mainControllerSen = 5;

    [Header("Graphic Settings")]
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private float defaultBrightness = 1.0f;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    
    [SerializeField] private Brightness brightnessController;


    [SerializeField] private TMP_Dropdown qualityDropdown = null;

    private int _qualityLevel;
    private float _brightnessLevel;

    [Header("Resolution Settings")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private int resolutionIndex;

    [Header("Levels to Load")]
    public string _newGameLevel;

    public void Start(){
        // Clear the existing options.
        resolutionDropdown.ClearOptions();
        
        // Create a new list with one option.
        List<string> options = new List<string>();
        options.Add("1920 x 1080");
        
        // Add the option to the dropdown.
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = 0;
        resolutionDropdown.RefreshShownValue();
        
        // Optionally, set resolutionIndex to 0
        resolutionIndex = 0;
        SetResolution(resolutionIndex);
        
        // Initialize brightness settings
        brightnessSlider.value = defaultBrightness;
        brightnessTextValue.text = defaultBrightness.ToString("0.0");
        PlayerPrefs.SetFloat("masterBrightness", defaultBrightness);

        float newVolume2 = defaultVolume / 1000f;
        volumeTextValue.text = newVolume2.ToString("0.0");
        volumeSlider.value = newVolume2;
        PlayerPrefs.SetFloat("masterVolume", newVolume2);
        
        // Apply the brightness to the post-processing
        if(brightnessController != null)
        {
            brightnessController.SetBrightness(defaultBrightness);
        }
      
    }


    public void SetResolution(int dummyIndex){
    // Set the resolution to 1920 x 1080 regardless of input.
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
}


    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void ExitButton(){
        Application.Quit();
    }
    
    public void SetVolume(float volume){

        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply(){
    // Convert the slider value to the normalized volume.
    float newVolume = volumeSlider.value / 1000f;
    AudioManager.instance.SetVolume(newVolume);
    // Optionally, update the text display:
    volumeTextValue.text = volumeSlider.value.ToString("0.0");
    PlayerPrefs.SetFloat("masterVolume", newVolume);
    StartCoroutine(ConfirmationBox());
}

    public void VolumeSlider(){
        volumeTextValue.text = volumeSlider.value.ToString("0.0");
    }
    
    public IEnumerator ConfirmationBox(){
        confirmationPrompt.SetActive(true);

        yield return new WaitForSeconds(2);

        confirmationPrompt.SetActive(false);
    }
    
    public void ResetButton(string MenuType){
        if(MenuType == "Audio"){
            //AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            //VolumeApply();
        }
        if(MenuType == "Gameplay"){
            ControllerSenSlider.value = defaultControllerSen;
            ControllerSenTextValue.text = defaultControllerSen.ToString("0");
            mainControllerSen = defaultControllerSen;
            //GameplayApply();
        }
        if(MenuType == "Graphics"){
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);
            qualityDropdown.RefreshShownValue();

            Resolution currentResolution = Screen.currentResolution;
            //Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            //GraphicsApply();
        }
    }

    public void onOpenMenuPress(){
        float volume = PlayerPrefs.GetFloat("masterVolume", 0.005f);
        // Convert volume back to slider value (0.005 becomes 5, 0.01 becomes 10, etc.)
        float sliderValue = volume * 1000f;
        volumeTextValue.text = sliderValue.ToString("0.0");
        volumeSlider.value = sliderValue;

        float brightness = PlayerPrefs.GetFloat("masterBrightness", 1.0f);
        brightnessTextValue.text = brightness.ToString("0.0");
        brightnessSlider.value = brightness;

        int quality = PlayerPrefs.GetInt("masterQuality", 1);
        qualityDropdown.value = quality;

        int currentResolutionIndex2 = PlayerPrefs.GetInt("resolutionIndex", 1);
        resolutionDropdown.value = currentResolutionIndex2;

    }

    public void returnToMainMenu(){
    // Reset time scale in case it was paused.
        Time.timeScale = 1f;
    // Load the main menu scene. Replace "MainMenu" with the actual scene name if different.
        SceneManager.LoadScene("MainMenuScene");
}


    public void SetControllerSen(float sensitivity){
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        ControllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply(){
        PlayerPrefs.SetInt("mainControllerSen", mainControllerSen);
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness){
    
    _brightnessLevel = brightness;
    brightnessTextValue.text = brightness.ToString("0.0");
    
}

    public void SetQuality(int qualityIndex){
        _qualityLevel = qualityIndex;
        
    }

    public void GraphicsApply(){
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);


        // Instead of accessing the resolutions array, create a new Resolution manually.
        Resolution chosenRes = new Resolution();
        chosenRes.width = 1920;
        chosenRes.height = 1080;
        
        Screen.SetResolution(chosenRes.width, chosenRes.height, true);

        if(brightnessController != null)
        {
            brightnessController.SetBrightness(_brightnessLevel);
        }

        StartCoroutine(ConfirmationBox());
    }

    public void BrightnessSlider(){
        brightnessTextValue.text = brightnessSlider.value.ToString("0.0");
    }
 

}