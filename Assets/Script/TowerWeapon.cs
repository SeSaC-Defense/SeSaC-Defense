using System.Collections;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttackToTarget }

public class TowerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject     projectilePrefab;
    [SerializeField] private Transform      spawnPoint;
    [SerializeField] private float          attackRate      = 1.0f;
    [SerializeField] private float          attackRange     = 10.0f;

    private WeaponState     weaponState     = WeaponState.SearchTarget;
    private Transform       attackTarget    = null;
    private EnemySpawner    enemySpawner;

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

    private void RotateToTarget() // 적의 위체에 따른 타워의 방향 설정
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
            float closestDistSqr = Mathf.Infinity;
            for(int i = 0; i < enemySpawner.EnemyList.Count; ++i) 
            {
                float distansce = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                if(distansce <= attackRange && distansce <= closestDistSqr)
                {
                    closestDistSqr  = distansce;
                    attackTarget    = enemySpawner.EnemyList[i].transform;
                }
            }
            if (attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }
            yield return null;
        }
        
    }
    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            if(attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget); 
                break;
            }

            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if (distance > attackRange)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget); 
                break;
            }
            yield return new WaitForSeconds(attackRate);

            SpawnProjectile();
        }
    }
    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint);
        clone.GetComponent<Projectile>().Setup(attackTarget);
    }
}
