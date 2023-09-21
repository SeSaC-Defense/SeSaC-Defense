using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMask : MonoBehaviour
{
    [SerializeField]
    private UIActiveToggle maskBlack;
    [SerializeField]
    private UIActiveToggle maskTransparent;

    void Start()
    {
        UIStateEventHandler.OnStateChanged += OnUIStateChanged;
    }

    public void OnUIStateChanged(UIStateType state)
    {
        switch (state)
        {
            case UIStateType.BarrackPressedOnWaiting:
            case UIStateType.ConstructionConfirming:
            case UIStateType.DestructionConfirming:
                maskBlack.OpenUI();
                maskTransparent.CloseUI();
                break;
            case UIStateType.BuildingPressed:
                maskBlack.CloseUI();
                maskTransparent.OpenUI();
                break;
            default:
                maskBlack.CloseUI();
                maskTransparent.CloseUI();
                break;
        }
    }

    public void CloseMaskAndButtonGroup()
    {
        UIStateEventHandler.Instance.ChangeState(UIStateType.None);
    }
}
