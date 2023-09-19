using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHp = 2;

    private float currentHP;
    private bool isDie = false;
    private Enemy enemy;
    public float MaxHP => maxHp;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        currentHP = maxHp;
    }

    public void TakeDamage(float damage)
    {
        if (isDie == true) return;

        currentHP -= damage;

        if( currentHP <= 0 ) 
        {
            isDie = true;
            enemy.OnDie(EnemyDestroyType.Kill);
        }

    }
}
