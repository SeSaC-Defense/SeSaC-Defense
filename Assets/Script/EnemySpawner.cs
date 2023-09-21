using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    UnitProgressBar unitProgressBar;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private Player player;
    [SerializeField]
    private PlayerGold playerGold;
    
    private List<Enemy> enemyList;

    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {
        enemyList = new List<Enemy>();
        StartCoroutine("SpawnEnemy");
    }

    private void Update()
    {
        float nearestEnemyPosition = 100;

        foreach (Enemy enemy in enemyList)
        {
            float currentEnemyPosition = enemy.transform.position.x;
            if (currentEnemyPosition < nearestEnemyPosition)
            {
                nearestEnemyPosition = currentEnemyPosition;
            }
        }

        unitProgressBar.EnemyUnitPositionX = nearestEnemyPosition;
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject Clone = Instantiate(enemyPrefab);
            Enemy enemy = Clone.GetComponent<Enemy>();
            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, int gold)
    {
        if(type == EnemyDestroyType.Arrive)
        {
            player.TakeDamage(1);
        }
        else if(type == EnemyDestroyType.Kill)
        {
            playerGold.CurrentGold += gold;
        }
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
