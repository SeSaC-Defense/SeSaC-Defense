using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public enum UnitDestroyType { Kill = 0, Arrive }

public class Unit : MonoBehaviour
{
    private int wayPointCount;      //이동 경로 개수
    private Transform[] wayPoints;      //이동 경로 정보
    private int currentIndex = 0;   //현재 목표지점 인덱스
    private Movement2D movement2D;         //오브젝트 이동 제어

    public void SetUp(Transform[] wayPoints, Transform spawnPoint)
    {
        movement2D = GetComponent<Movement2D>();
        this.wayPoints = wayPoints;
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
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if(currentIndex < wayPointCount - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex ++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            PlayerUnitList.Instance.UnitList.Remove(this);
            OnDie(); //다음 waypoint가 존재하지않으면 유닛 삭제
        }
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }
}
