using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip[] audioClips;
    public void Play(int ix)
    {
        if (ix < 0 || ix >= audioClips.Length)
            return;
        SFXManager.Instance.PlaySound(audioClips[ix]);
    }
}
