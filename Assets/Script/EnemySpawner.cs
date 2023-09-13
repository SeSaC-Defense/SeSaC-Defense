using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject     enemyPrefab; 
    [SerializeField] private float          spawnTime;
    [SerializeField] private Transform[]    wayPoints;
    private List<Enemy> enemyList;

    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {
        enemyList = new List<Enemy>(); //积己等 蜡粗阑 历厘
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy() //积己肺流
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
    public void DestroyEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
