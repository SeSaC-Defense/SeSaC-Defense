using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGroupBuilding : MonoBehaviour
{
    public void OnDestruct()
    {
        GetComponent<UIActiveToggle>().CloseUI();
        Destroy(ObjectDetector.Instance.HitTransform.gameObject);
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
