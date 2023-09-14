using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVO : MonoBehaviour, IToggleSliderVO, IToggleSliderAction
{
    public bool isOn = true;
    public float volume = 1f;

    public bool GetOptionValueBool()
    {
        return isOn;
    }

    public float GetOptionValueFloat()
    {
        return volume;
    }

    public void SetOptionValue(bool value)
    {
        isOn = value;
    }

    public void SetOptionValue(float value)
    {
        volume = value;
    }

    public void ToggleSliderAction()
    {
        gameObject.GetComponent<ISoundManager>().UpdateMute();
        gameObject.GetComponent<ISoundManager>().UpdateVolume();
    }
}
