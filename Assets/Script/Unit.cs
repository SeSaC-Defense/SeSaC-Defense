using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitData[] unitDatas;
    private UnitData unitData;

    private int playerNumber = 1;
    public int PlayerNumber => playerNumber;

    private Transform[] wayPoints;          //이동 경로 정보
    private Transform spawnPoint;
    private Movement2D movement2D;          //오브젝트 이동 제어
    private SpriteRenderer spriteRenderer;
    private int wayPointCount;              //이동 경로 개수
    private int currentIndex = 0;           //현재 목표지점 인덱스
    
    public PlayerUnitList playerUnitList;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void SetUp(Transform[] wayPoints, Transform spawnPoint, PlayerUnitList playerUnitList, UnitData unitData) //유닛 정보처리
    {
        this.unitData = unitData;
        this.playerUnitList = playerUnitList;
        spriteRenderer.sprite = unitData.Sprite;
        gameObject.name = unitData.UnitName;
        movement2D = GetComponent<Movement2D>();
        this.wayPoints = wayPoints;
        wayPointCount = wayPoints.Length; //유닛 이동 경로 waypoint 정보 설정

        for (int i = 0; i < wayPointCount; i++) //waypoints중에서 spawnpoint와 같은 waypoint를 찾아낸다
        {
            if (wayPoints[i] == spawnPoint)
            {
                currentIndex = i;
                break; // 일치하는 인덱스를 찾으면 루프 종료
            }
        }
        transform.position = spawnPoint.position;    //유닛의 첫 위치를 지정하는
        StartCoroutine("OnMove");                    //적 이동/목표지점 설정 코루틴 시작
    }

    private IEnumerator OnMove() //waypoint로 이동한다
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo(); //도착하면 다음 waypoint를 찾는다
            }
            yield return null;
        }
    }

    private void NextMoveTo() //현재 waypoint의 +1의 index를 가진 waypoint 찾기
    {
        if (currentIndex < wayPointCount - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
            movement2D.DataSetting(unitData);
        }
        else
        {
            playerUnitList.DestroyUnit(this); //없다면 유닛 삭제 로직
        }
    }
}
