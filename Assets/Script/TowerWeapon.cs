using System.Collections;
using UnityEngine;

public enum WeaponType {  Canon }
public enum WeaponState { SearchTarget = 0, TryAttackCannon }

public class TowerWeapon : MonoBehaviour
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

    private WeaponState weaponState     = WeaponState.SearchTarget;
    private Transform attackTarget    = null;
    private EnemySpawner enemySpawner;
    

    public void Setup(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;
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

            if( attackTarget != null)
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
        for ( int i = 0; i < enemySpawner.EnemyList.Count; ++i)
        {
            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
            if ( distance <= attackRange && distance <= ClosestDistSqr)
            {
                ClosestDistSqr = distance;
                attackTarget = enemySpawner.EnemyList[i].transform;
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
        GameObject clone = Instantiate(projectilePrefab, spawnPoint);
        clone.GetComponent<Projectile>().Setup(attackTarget);
    }
}
