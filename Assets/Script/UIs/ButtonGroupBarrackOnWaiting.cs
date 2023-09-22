using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGroupBarrackOnWaiting : MonoBehaviour
{
    [SerializeField]
    private GameObject[] unitPrefabs;
    private ICommand unitCommand;
    public GameObject[] UnitPrefabs { get => unitPrefabs; }
    
    public void OnSelectUnit(int ix)
    {
        GetComponent<UIActiveToggle>().CloseUI();
        ICommand unitCommand = new UnitSpawn(ObjectDetector.Instance.HitIndex, ix);
        unitCommand.Execute();
        //ICommand unitCommand = new UnitSpawn(ObjectDetector.Instance.hitIndex, ix);
        //TurnManager.Instance.PushCommand(unitCommand);
        GetComponent<UIStateToggle>().OnToggle();
    }

    public void OnDestruct()
    {
        GetComponent<UIActiveToggle>().CloseUI();
        ICommand tearDownCommand = new TearDown(ObjectDetector.Instance.HitIndex);
        tearDownCommand.Execute();
        Tile tile = ObjectDetector.Instance.HitTransform.parent.GetComponent<Tile>();
        tile.IsBuildTower = false;
        // TODO: 재화 반환
        GetComponent<UIStateToggle>().OnToggle();
    }
    
    public void OnCancel()
    {
        GetComponent<UIActiveToggle>().CloseUI();
        GetComponent<UIStateToggle>().OnToggle();
    }
}
