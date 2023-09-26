using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public enum WeaponType { Canon }
public enum WeaponState { SearchTarget = 0, TryAttackCannon }

public class TowerWeapon : NetworkBehaviour
{
    [Header("Commons")]
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private WeaponType Weapontype;
    
    [Header("Magic")]
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float attackRate = 1.0f;
    [SerializeField]
    private float attackRange = 5f;

    private WeaponState weaponState = WeaponState.SearchTarget;
    private Transform attackTarget = null;

    public int PlayerNo => IsOwnedByServer ? 0 : 1;
    private IReadOnlyList<Transform> EnemyUnitList => PlayerUnitList.Instance.GetEnemyUnitList(PlayerNo);

    public void Setup()
    {
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
        if(gameObject.transform.position.x > attackTarget.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {
            attackTarget = FindClosestAttackTarget();

            if ( attackTarget != null)
            {
                ChangeState(WeaponState.TryAttackCannon);
            }
            yield return null;
        }
    }
    private IEnumerator TryAttackCannon()
    {
        while (true)
        {
            if( IsPossibleToAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            SpawnProjectileServerRpc(OwnerClientId, spawnPoint.position, attackTarget.position);

            yield return new WaitForSeconds(attackRate);
        }
    }

    private Transform FindClosestAttackTarget()
    {
        float ClosestDistSqr = Mathf.Infinity;
        for ( int i = 0; i < EnemyUnitList.Count; ++i)
        {
            float distance = Vector3.Distance(EnemyUnitList[i].transform.position, transform.position);
            if ( distance <= attackRange && distance <= ClosestDistSqr)
            {
                ClosestDistSqr = distance;
                attackTarget = EnemyUnitList[i].transform;
            }
        }
        return attackTarget;
    }
    private bool IsPossibleToAttackTarget()
    {
        if (attackTarget == null)
            return false;

        float distance = Vector3.Distance(attackTarget.position, transform.position);
        if (distance > attackRange)
        {
            attackTarget = null;
            return false;
        }

        return true;
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnProjectileServerRpc(ulong clientId, Vector3 spawnPosition, Vector3 targetPosition)
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        NetworkObject networkObject = clone.GetComponent<NetworkObject>();
        networkObject.SpawnWithOwnership(clientId);
        
        clone.GetComponent<Projectile>().Setup(targetPosition);
    }
}
