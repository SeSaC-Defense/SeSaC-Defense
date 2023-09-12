using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundManager
{
    public void PlaySound(AudioClip audio);
    public void UpdateVolume();
    public void UpdateMute();
}
