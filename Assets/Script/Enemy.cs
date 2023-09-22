using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UIElements;

public enum EnemyDestroyType { Kill = 0, Arrive }

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int gold = 1;

    private int wayPointCount;      //이동 경로 개수
    private Transform[] EnemywayPoints;      //이동 경로 정보
    private int currentIndex = 0;   //현재 목표지점 인덱스
    private Movement2D movement2D;         //오브젝트 이동 제어

    public void SetUp(Transform[] wayPoints, Transform spawnPoint)
    {
        movement2D = GetComponent<Movement2D>();
        this.EnemywayPoints = wayPoints;
        wayPointCount = wayPoints.Length;         //유닛 이동 경로 waypoint 정보 설정

        for (int i = 0; i < wayPointCount; i++)
        {
            if (wayPoints[i] == spawnPoint)
            {
                currentIndex = i;
                break;                              // 일치하는 인덱스를 찾으면 루프 종료
            }
        }

        transform.position = spawnPoint.position;    //유닛의 첫 위치를 지정하는 

        StartCoroutine("OnMove");                    //적 이동/목표지점 설정 코루틴 시작
    }

    private IEnumerator OnMove()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, EnemywayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if (currentIndex < wayPointCount - 1)
        {
            transform.position = EnemywayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (EnemywayPoints[currentIndex].position - transform.position).normalized;
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
        EnemyController.Instance.DestroyEnemy(type, this, gold);
    }
}
