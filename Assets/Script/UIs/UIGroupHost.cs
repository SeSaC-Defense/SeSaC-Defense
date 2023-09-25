using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGroupHost : MonoBehaviour
{
    [SerializeField]
    RelayManager relayManager;
    [SerializeField]
    TextMeshProUGUI textJoinCode;
    [SerializeField]
    Button btnStart;

    public void SetJoinCodeText(string text)
    {
        textJoinCode.text = text;
    }

    public void SetStartButtonInteractable(bool interactable)
    {
        btnStart.interactable = interactable;
    }

    public void OnBtnJoinCodePressed()
    {
        relayManager.GetJoinCode();
    }

    public void OnBtnStartPressed()
    {
        relayManager.OnStartGameHost();
    }
}
