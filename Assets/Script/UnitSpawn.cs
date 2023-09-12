using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawn : MonoBehaviour
{
    [SerializeField] private GameObject  unit1;
    [SerializeField] private GameObject  unit2;
    [SerializeField] private GameObject  unit3;
    [SerializeField] private GameObject  unit4;
    [SerializeField] private float       spawnTime;

    private GameObject spawnUnit; //스폰될 유닛
    private Transform waypoint;
    private List<Unit> unitList;
    public List<Unit> UnitList => unitList;

    private void Awake()
    {
        unitList = new List<Unit>();
    }
    private void Start()
    {
        WayPointScan();
    }
    public void WayPointScan()
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
                this.waypoint = wayPoint.transform;
            }
        }
    }
    
    public void UnitChoice(string n)
    {
        switch (n) //각 4가지의 버튼에 따라 생성될 유닛 설정
        {
            case "1":
                spawnUnit = unit1;
                break;
            case "2":
                spawnUnit = unit2;
                break;
            case "3":
                spawnUnit = unit3;
                break;
            case "4":
                spawnUnit = unit4;
                break;
            default:
                break;
        }
        StartCoroutine("SpawnUnit");
    }

    private IEnumerator SpawnUnit()
    {
        while (true)
        {
            GameObject clone = Instantiate(spawnUnit); //유닛 생성
            Unit unit = clone.GetComponent<Unit>();
            unit.SetUp(waypoint);                      //생성 유닛 정보처리 시작

            unitList.Add(unit);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
