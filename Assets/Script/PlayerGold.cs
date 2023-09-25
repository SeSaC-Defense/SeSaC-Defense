using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    //[SerializeField]
    //private UICount goldText;
    [SerializeField] private int currentGold = 10;

    public int CurrentGold
    {
        set {
            //goldText.SetValue(value);
            currentGold = Mathf.Max(0, value);
        }
        get => currentGold;
    }
}
