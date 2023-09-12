using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public enum WeaponState { SearchTarget = 0, AttackToTarget }
public class TowerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float attackRate = 1.0f;
    [SerializeField] private int attackDamage = 0;

    private WeaponState weaponState = WeaponState.SearchTarget;
    private Transform attackTarget = null;
    //private EUnitSpawn eUnitSpawn;

    public void SetUp(/*EUnitSpawn*/)
    {
        //this.EUnitSpawn = 매개변수

        ChangeState(WeaponState.SearchTarget);
    }

    private void ChangeState(WeaponState NewState)
    {
        StopCoroutine(weaponState.ToString());
        weaponState = NewState;
        StartCoroutine(weaponState.ToString());
    }

    private void Update()
    {
        if (attackTarget != null)
        {
            RotateToTarget();
        }
    }

    private void RotateToTarget()
    {
        float dx = attackTarget.position.x - transform.position.x;
        float dy = attackTarget.position.y - transform.position.y;
        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private IEnumerator SearchTarget()
    {
        yield return null;
    }
    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            yield return null;
        }
    }
    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(bullet, spawnPoint);
        clone.GetComponent<BulletAttack>().SetUp(attackTarget, attackDamage);

    }
}
