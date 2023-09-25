using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGroupBarrackOnWaiting : MonoBehaviour
{
    [SerializeField]
    private GameObject[] unitPrefabs;
    public GameObject[] UnitPrefabs { get => unitPrefabs; }
    public void OnSelectUnit(int ix)
    {
        GetComponent<UIActiveToggle>().CloseUI();
        UnitSpawner spawner = ObjectDetector.Instance.HitTransform.GetComponent<UnitSpawner>();
        spawner.UnitChoice(ix);
        GetComponent<UIStateToggle>().OnToggle();
    }

    public void OnDestruct()
    {
        GetComponent<UIActiveToggle>().CloseUI();
        Destroy(ObjectDetector.Instance.HitTransform.gameObject);
        Tile tile = ObjectDetector.Instance.HitTransform.parent.GetComponent<Tile>();
        tile.HasBuilding = false;
        // TODO: 재화 반환
        GetComponent<UIStateToggle>().OnToggle();
    }

    public void OnCancel()
    {
        GetComponent<UIActiveToggle>().CloseUI();
        GetComponent<UIStateToggle>().OnToggle();
    }
}
