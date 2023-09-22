using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHP : MonoBehaviour
{
    [SerializeField]
    private float maxHp = 2;

    private float currentHP;
    private bool isDie = false;
    private Unit unit;
    public float MaxHP => maxHp;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        currentHP = maxHp;
    }

    public void TakeDamage(float damage)
    {
        if (isDie == true) return;

        currentHP -= damage;

        if (currentHP <= 0)
        {
            isDie = true;
            unit.OnDie();
        }

    }
}
