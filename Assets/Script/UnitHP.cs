using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHP : MonoBehaviour
{
    [SerializeField]
    private UnitData[] unitDatas;

    private int playerNumber = 1;
    public int PlayerNumber => playerNumber;

    private float currentHP;
    public float CurrentHP => currentHP;

    //public float MaxHP => maxHp;
    //private float maxHp;
    private Unit unit;
    private UnitData unitData;
    private bool isDie = false;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public void SetUp(UnitData unitData) //기본 정보 처리
    {
        this.unitData = unitData;
        currentHP = unitData.UnitHP;
    }

    public void TakeDamage(float damage) //맞을 경우 HP를 깎는다
    {
        if (isDie == true) return;

        currentHP -= damage;

        if (currentHP <= 0)
        {
            isDie = true;
            PlayerGold.Instance.Kill(1);
            unit.playerUnitList.DestroyUnit(unit); // 체력이 0보다 밑이면 유닛 삭제
        }

    }
}
