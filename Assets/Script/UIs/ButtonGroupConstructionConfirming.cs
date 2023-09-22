using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroupConstructionConfirming : MonoBehaviour
{
    [SerializeField]
    private Sprite[] towerImages;
    public Sprite[] TowerImages { get => towerImages; }

    int i = 1;
    public void OnConfirm()
    {
        GetComponent<UIActiveToggle>().CloseUI();
        ICommand towerCommand = new TowerSpawn(ObjectDetector.Instance.HitIndex, i);
        towerCommand.Execute();
        Tile tile = ObjectDetector.Instance.HitTransform.GetComponent<Tile>();
        tile.IsBuildTower = false;
        GetComponent<UIStateToggle>().OnToggle();
    }

    public void OnCancel()
    {
        GetComponent<UIActiveToggle>().CloseUI();
        GetComponent<UIStateToggle>().OnToggle();
    }
}
