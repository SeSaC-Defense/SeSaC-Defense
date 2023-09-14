using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIStateType
{
    None,
    ConstructionChecking,
    ConstructionConfirming,
    TowerPressed,
    BarrackPressedOnWaiting,
    BarrackPressedOnProducing,
    DestructionConfirming,
    Config,
    EnemyBase
}
public class UIStateEventHandler : Singleton<UIStateEventHandler>
{
    public delegate void UIStateHandler(UIStateType state);
    public static event UIStateHandler OnStateChanged;
    public UIStateType CurrentState { get; private set; }

    private void Start()
    {
        CurrentState = UIStateType.None;
        OnStateChanged += UIStateEventHandler_OnStateChange;
    }

    private void UIStateEventHandler_OnStateChange(UIStateType state)
    {
        CurrentState = state;
    }

    public void ChangeState(UIStateType state)
    {
        OnStateChanged(state);
    }

}
