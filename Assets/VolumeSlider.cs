using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioManager audioManager;
    
    Slider slider;
    void Start()
    {
        GameObject audiomanagerObj = GameObject.FindGameObjectWithTag("AudioManager");
        audioManager = audiomanagerObj.GetComponent<AudioManager>();
        slider = GetComponent<Slider>();
        Debug.Log(audioManager.getDefaultVolume());
        slider.value = audioManager.getDefaultVolume();

    }

    public void OnValueChanged(float value){
        Debug.Log(value);
        audioManager.changeDefaultVolume(value);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
