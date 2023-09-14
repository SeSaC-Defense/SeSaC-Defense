using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateToggle : MonoBehaviour
{
    [SerializeField]
    private UIStateType uiStateType;
    public void OnToggle()
    {
        UIStateEventHandler.Instance.ChangeState(uiStateType);
    }
}
