using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGroupBuilding : MonoBehaviour
{
    public void OnDestruct()
    {
        GetComponent<UIActiveToggle>().CloseUI();
        Building building = ObjectDetector.Instance.HitTransform.GetComponent<Building>();
        building.DestructServerRpc();
        // TODO: ��ȭ ��ȯ
        GetComponent<UIStateToggle>().OnToggle();
    }

    public void OnCancel()
    {
        GetComponent<UIActiveToggle>().CloseUI();
        GetComponent<UIStateToggle>().OnToggle();
    }
}
