using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UIElements;

public enum EnemyDestroyType { Kill = 0, Arrive }
public class Enemy : MonoBehaviour
{
    [SerializeField] int gold = 1;
    private int             wayPointCount;
    private Transform[]     wayPoints;
    private int             currentIndex = 0;
    private Movement2D      movement2D;
    private EnemySpawner    enemySpawner;

    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoint)
    {
        movement2D          = GetComponent<Movement2D>();
        this.enemySpawner   = enemySpawner;
        wayPointCount       = wayPoint.Length;
        this.wayPoints      = new Transform[wayPointCount];
        this.wayPoints      = wayPoint;
        transform.position  = wayPoint[currentIndex].position;

        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if(currentIndex < wayPoints.Length - 1) 
        { 
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            gold = 0;
            OnDie(EnemyDestroyType.Arrive);
        }
    }
    public void OnDie(EnemyDestroyType type)
    {
        enemySpawner.DestroyEnemy(type, this, gold);
    }
}
