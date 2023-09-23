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

    public void SetUp(UnitData unitData) //�⺻ ���� ó��
    {
        this.unitData = unitData;
        currentHP = unitData.UnitHP;
    }

    public void TakeDamage(float damage) //���� ��� HP�� ��´�
    {
        if (isDie == true) return;

        currentHP -= damage;

        if (currentHP <= 0)
        {
            isDie = true;
            PlayerGold.Instance.Kill(1);
            unit.playerUnitList.DestroyUnit(unit); // ü���� 0���� ���̸� ���� ����
        }

    }
}
