using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroupConstructionConfirming : MonoBehaviour
{
    [SerializeField]
    private ButtonGroupToggle buttonGroupToggle;
    [SerializeField]
    private Sprite[] towerImages;
    public Sprite[] TowerImages { get => towerImages; }

    public void OnConfirm()
    {
        GetComponent<UIActiveToggle>().CloseUI();
        GetComponent<UIStateToggle>().OnToggle();
        TowerSpawner.Instance.SpawnTower(buttonGroupToggle.HitTransform);
    }

    public void OnCancel()
    {
        GetComponent<UIActiveToggle>().CloseUI();
        GetComponent<UIStateToggle>().OnToggle();
    }

    private void OnUIStateChanged(UIStateType state)
    {
        if (state != UIStateType.ConstructionConfirming)
            return;

        Sprite sprite = towerImages[(int)TowerSpawner.Instance.TowerChosen];
        transform.Find("Image").GetComponent<Image>().sprite = sprite;
    }
}
