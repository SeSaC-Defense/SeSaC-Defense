using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMInitializer : SoundInitializer
{
    private void FixedUpdate()
    {
        BGMManager.Instance.gameObject.GetComponent<SoundVO>().isOn = isOn;
        BGMManager.Instance.gameObject.GetComponent<SoundVO>().volume = volume;
        BGMManager.Instance.UpdateMute();
        BGMManager.Instance.UpdateVolume();
        Destroy(gameObject);
    }
}
