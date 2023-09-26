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
    TextMeshProUGUI textPlayerConnected;

    public void SetJoinCodeText(string text)
    {
        textJoinCode.text = text;
    }

    public void SetPlayerConnectedText(string text)
    {
        textPlayerConnected.text = text;
    }

    public void OnBtnStartPressed()
    {
        relayManager.OnStartGameHost();
    }
}
