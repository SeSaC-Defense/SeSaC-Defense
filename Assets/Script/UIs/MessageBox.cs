using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMessage;

    public void SetMessage(string message)
    {
        textMessage.text = message;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
