using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXInitializer : SoundInitializer
{
    private void FixedUpdate()
    {
        SFXManager.Instance.gameObject.GetComponent<SoundVO>().isOn = isOn;
        SFXManager.Instance.gameObject.GetComponent<SoundVO>().volume = volume;
        SFXManager.Instance.UpdateMute();
        SFXManager.Instance.UpdateVolume();
        Destroy(gameObject);
    }
}
