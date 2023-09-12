using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern;

public abstract class SoundInitializer : MonoBehaviour
{
    [SerializeField]
    protected bool isOn;
    [Range (0.0f, 1.0f)]
    [SerializeField]
    protected float volume;

}
