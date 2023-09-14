using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActiveToggle : MonoBehaviour
{
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }
}
