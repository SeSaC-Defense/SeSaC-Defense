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

    private GameObject spawnUnit; //������ ����
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
        switch (n) //�� 4������ ��ư�� ���� ������ ���� ����
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
            GameObject clone = Instantiate(spawnUnit); //���� ����
            Unit unit = clone.GetComponent<Unit>();
            unit.SetUp(waypoint);                      //���� ���� ����ó�� ����

            unitList.Add(unit);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
