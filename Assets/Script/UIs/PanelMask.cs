using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMask : MonoBehaviour
{
    [SerializeField]
    private UIActiveToggle uiActiveToggle;

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
                uiActiveToggle.OpenUI();
                break;
            default:
                uiActiveToggle.CloseUI();
                break;
        }
    }

    public void CloseMaskAndButtonGroup()
    {
        UIStateEventHandler.Instance.ChangeState(UIStateType.None);
    }
}
