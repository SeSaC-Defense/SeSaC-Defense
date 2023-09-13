using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackDataViewer : MonoBehaviour
{
    private Transform hit;

    public void SelectedBuilding(Transform hit) //���õ� barrack�� ������ ����
    {
        this.hit = hit;
    }

    public void ChoiceUnit(int n) //���õ� barrackController�� ���� int������ ���� ����
    {
        hit.GetComponent<BarrackController>().UnitChoice(n);
        gameObject.SetActive(false); //������ ���õǸ� ����ȭ
    }
}
