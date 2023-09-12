using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern;

public class SFXManager : Singleton<SFXManager>, ISoundManager
{
    private AudioSource audioSource;
    private SoundVO soundVO;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        soundVO = GetComponent<SoundVO>();
    }

    public void PlaySound(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }

    public void UpdateVolume()
    {
        audioSource.volume = soundVO.volume;
    }

    public void UpdateMute()
    {
        audioSource.mute = !soundVO.isOn;
    }

}