using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackDataViewer : MonoBehaviour
{
    private Transform hit;

    public void SelectedBuilding(Transform hit) //선택된 barrack의 정보를 받음
    {
        this.hit = hit;
    }

    public void ChoiceUnit(int n) //선택된 barrackController에 유닛 int값으로 유닛 선택
    {
        hit.GetComponent<BarrackController>().UnitChoice(n);
        gameObject.SetActive(false); //유닛이 선택되면 가시화
    }
}
