using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICount : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private int textLength = 1;

    private void Awake()
    {
        text.text = "0";
    }

    public void SetValue(int value)
    {
        text.text = value.ToString(new string('0', textLength));
    }
}
