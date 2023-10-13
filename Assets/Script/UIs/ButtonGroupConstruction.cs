using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroupConstruction : MonoBehaviour
{
    [SerializeField]
    private Image[] buttonImages;
    private int currentButtonIndex = -1;

    private void Start()
    {
        UIStateEventHandler.OnStateChanged += OnUIStateChanged;
    }

    public void OnButtonPressed(int ix)
    {
        OnButtonReleased();

        if (currentButtonIndex == ix)
        {
            UIStateEventHandler.Instance.ChangeState(UIStateType.None);
            return;
        }

        TowerSpawner towerSpawner = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<TowerSpawner>();
        towerSpawner.SetTowerType(ix);

        currentButtonIndex = ix;
        buttonImages[currentButtonIndex].color = Color.gray;
    }
    public void OnButtonReleased()
    {
        if (currentButtonIndex != -1)
            buttonImages[currentButtonIndex].color = Color.white;
    }

    public void OnUIStateChanged(UIStateType state)
    {
        if (UIStateType.ConstructionChecking != state)
        {
            OnButtonReleased();
            currentButtonIndex = -1;
        }
    }
}
