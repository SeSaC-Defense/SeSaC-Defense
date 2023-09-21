using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] unit;
    [SerializeField]
    private float spawnTime;

    private GameObject spawnUnit; //스폰될 유닛
    private Transform spawnPoint;
    private Transform[] waypoints;

    //private List<Unit> unitList;
    //public List<Unit> UnitList => unitList;

    public bool InOperation
    {
        set; get;
    }

    private void Awake()
    {
        //unitList = new List<Unit>();
        InOperation = false;

    }
    private void Start()
    {
        WayPointScan();
    }

    public void Setup(Transform[] waypoints)
    {
        this.waypoints = waypoints;
    }

    public void WayPointScan() //waypoint중 가장 가까운것 탐색
    {
        GameObject[] wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");

        float closestDistance = float.MaxValue;
        Transform towerTransform = transform;

        foreach (GameObject wayPoint in wayPoints)
        {
            float distance = Vector3.Distance(towerTransform.position, wayPoint.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                this.spawnPoint = wayPoint.transform; //가장 가까운 waypoint 저장
            }
        }
    }

    public void UnitChoice(int unit)
    {
        spawnUnit = this.unit[unit];
        InOperation = true;
        StartCoroutine("SpawnUnit");
    }

    private IEnumerator SpawnUnit()
    {
        while (true)
        {
            GameObject clone = Instantiate(spawnUnit); //유닛 생성
            Unit unit = clone.GetComponent<Unit>();
            unit.SetUp(waypoints, spawnPoint);                      //생성 유닛 정보처리 시작
            PlayerUnitList.Instance.UnitList.Add(unit);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
