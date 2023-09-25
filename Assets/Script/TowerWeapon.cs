using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public enum WeaponType {  Canon }
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
    private float attackRate  = 1.0f;
    [SerializeField]
    private float attackRange = 5f;

    private int playerNo;
    private WeaponState weaponState     = WeaponState.SearchTarget;
    private Transform attackTarget    = null;
    private IReadOnlyList<Unit> EnemyUnitList => PlayerUnitList.Instance.GetEnemyUnitList(playerNo);

    public void Setup(ulong clientId, int playerNo)
    {
        this.playerNo = playerNo;

        SpawnServerRpc(clientId);

        ChangeState(WeaponState.SearchTarget);
    }

    [ServerRpc]
    private void SpawnServerRpc(ulong clientId)
    {
        NetworkObject networkObject = GetComponent<NetworkObject>();
        networkObject.SpawnWithOwnership(clientId);
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
            SpawnProjectile();
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
        if( attackTarget == null)
        {
            return false;
        }

        float distance = Vector3.Distance(attackTarget.position, transform.position );
        if( distance > attackRange)
        {
            attackTarget = null;
            return false;
        }
        return true;
    }

    private void SpawnProjectile()
    {
        ulong clientId = NetworkManager.Singleton.LocalClientId;
        GameObject clone = Instantiate(projectilePrefab, spawnPoint);

        clone.GetComponent<Projectile>().Setup(clientId, playerNo, attackTarget);
    }
}
