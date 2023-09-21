using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private UICount hpText;
    private float maxHP = 10;
    private float currentHP;
    
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    private void Start()
    {
        hpText.SetValue((int)currentHP);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        hpText.SetValue((int)currentHP);

        if (currentHP <= 0 ) 
        {
            print("die");
            //³¡
        }
    }
}
