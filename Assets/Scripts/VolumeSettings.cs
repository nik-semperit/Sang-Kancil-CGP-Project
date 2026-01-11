using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider volumeSlider;

    public void SetMasterVolume()
    {
        float volume = volumeSlider.value;

        // This converts the 0-1 slider value to -80 to 0 decibels
        // We use 0.0001 as the floor because Log10 of 0 is undefined
        myMixer.SetFloat("MasterVol", Mathf.Log10(Mathf.Max(0.0001f, volume)) * 20);
    }
}
