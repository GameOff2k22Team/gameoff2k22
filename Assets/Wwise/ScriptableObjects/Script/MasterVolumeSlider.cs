using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSlider : MonoBehaviour
{

    public Slider slider;
    public float masterVolume;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpecificVolume(string whatValue)
    {
        float sliderValue = slider.value;

        if (whatValue == "Master")
        {
            masterVolume = slider.value;
            AkSoundEngine.SetRTPCValue("Master_Volume", masterVolume);
        }
    }
}
