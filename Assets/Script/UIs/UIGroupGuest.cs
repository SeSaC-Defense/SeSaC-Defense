using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGroupGuest : MonoBehaviour
{
    [SerializeField]
    RelayManager relayManager;
    [SerializeField]
    TMP_InputField inputJoinCode;
    [SerializeField]
    Button btnStart;

    public string GetJoinCodeText()
    {
        return inputJoinCode.text;
    }

    public void SetStartButtonInteractable(bool interactable)
    {
        btnStart.interactable = interactable;
    }

    public void OnBtnStartPressed()
    {
        relayManager.OnStartGameGuest();
    }
}
