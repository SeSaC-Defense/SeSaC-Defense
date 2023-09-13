using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackController : MonoBehaviour
{
    [SerializeField] private GameObject[] unit;
    [SerializeField] private float spawnTime;
    
    private Transform[] wayPoints;
    private GameObject  spawnUnit; //스폰될 유닛
    private Transform   spawnPoint;
    
    public bool         inOperation = false; //유닛이 이미 생성중인지 확인하는 요소

    private List<Unit> unitList;
    public List<Unit> UnitList => unitList;

    private void Awake()
    {
        unitList = new List<Unit>();
    }
    private void Start()
    {
        WayPointScan(); //배럭이 생성되면 가장 가까운 waypoint를 탐색
    }

    public void Setup(Transform[] wayPoints)
    {
        this.wayPoints = wayPoints;
    }
    public void WayPointScan()
    {
        GameObject[] wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");

        float closestDistance = float.MaxValue;
        Transform towerTransform = transform;

        foreach (GameObject wayPoint in wayPoints) //모든 wayPoint를 순서대로 가장 가까운 wayPoint를 탐색
        {
            float distance = Vector3.Distance(towerTransform.position, wayPoint.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                this.spawnPoint = wayPoint.transform; //가장 가까운 waypoint를 spawnPoint에 저장
            }
        }
    }

    public void UnitChoice(int unit)  //선택된 유닛의 int값 저장 및 유닛생성중으로 변환
    {
        spawnUnit       = this.unit[unit];
        inOperation     = true;
        StartCoroutine("SpawnUnit");
    }

    private IEnumerator SpawnUnit()
    {
        while (true)
        {
            GameObject  clone   = Instantiate(spawnUnit);       //유닛 생성
            Unit        unit    = clone.GetComponent<Unit>();   
            unit.Setup(wayPoints, spawnPoint);                  //생성 유닛 정보처리 시작
            unitList.Add(unit); //unitList에 유닛저장
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
