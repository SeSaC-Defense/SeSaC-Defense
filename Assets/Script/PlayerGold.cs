using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern;

public class PlayerGold : Singleton<PlayerGold>
{
    private int playerNumber = 1;
    public int PlayerNumber => playerNumber;

    [SerializeField]
    private UICount goldText;
    [SerializeField] private int currentGold = 10;

    public int CurrentGold
    {
        set {
            goldText.SetValue(value);
            currentGold = Mathf.Max(0, value);
        }
        get => currentGold;
    }
    public void Kill(int i)
    {
        currentGold += i;
    }
}
