using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRelayMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject panelHost;
    [SerializeField]
    private GameObject panelGuest;

    public void OnClickHost()
    {
        panelHost.SetActive(true);
        panelGuest.SetActive(false);
    }

    public void OnClickGuest()
    {
        panelHost.SetActive(false);
        panelGuest.SetActive(true);
    }
}
