using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern;

public class BGMManager : Singleton<BGMManager>, ISoundManager
{
    [SerializeField]
    private AudioClip[] audioClips;
    private AudioSource audioSource;
    private SoundVO soundVO;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
        audioSource.Play();
        soundVO = GetComponent<SoundVO>();
    }

    public void PlaySound(AudioClip audio)
    {
        audioSource.Play();
    }

    public void UpdateVolume()
    {
        audioSource.volume = soundVO.volume;
    }

    public void UpdateMute()
    {
        audioSource.mute = !soundVO.isOn;
    }

    public void ChangeTrackTo(int ix)
    {
        if (CheckTrackIndex(ix))
        {
            audioSource.clip = audioClips[ix];
            audioSource.Play();
        }
    }

    public bool CheckTrackIndex(int ix)
    {
        return ix < audioClips.Length && ix > -1;
    }

}
