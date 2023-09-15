using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroupConstructionConfirming : MonoBehaviour
{
    [SerializeField]
    private Sprite[] towerImages;
    public Sprite[] TowerImages { get => towerImages; }

    public void OnConfirm()
    {
        GetComponent<UIActiveToggle>().CloseUI();
        TowerSpawner.Instance.SpawnTower(ObjectDetector.Instance.HitTransform);
        GetComponent<UIStateToggle>().OnToggle();
    }

    public void OnCancel()
    {
        GetComponent<UIActiveToggle>().CloseUI();
        GetComponent<UIStateToggle>().OnToggle();
    }
}
