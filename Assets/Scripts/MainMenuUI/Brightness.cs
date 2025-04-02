using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class Brightness : MonoBehaviour
{
    public static Brightness instance;
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    AutoExposure exposure;
    // Start is called before the first frame update


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        brightness.TryGetSettings(out exposure);
        exposure.keyValue.value = 1.0f;
        
        // Initialize slider value
        if (brightnessSlider != null)
        {
            brightnessSlider.value = 1.0f;
        }
        
        // Update text value
        if (brightnessTextValue != null)
        {
            brightnessTextValue.text = "1.0";
        }
    }

    // Update is called once per frame
    public void SetBrightness(float brightness){
    
    if (exposure != null){
        if (brightness !=0){
            exposure.keyValue.value = brightness;
        }
        else {
            exposure.keyValue.value = 0.05f;
        }
    }
    
    // Update text when brightness changes
    if (brightnessTextValue != null)
    {
        brightnessTextValue.text = brightness.ToString("0.0");
    }
    }
}
