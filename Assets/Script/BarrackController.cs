using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackController : MonoBehaviour
{
    [SerializeField] private GameObject[] unit;
    [SerializeField] private float spawnTime;
    
    private Transform[] wayPoints;
    private GameObject  spawnUnit; //������ ����
    private Transform   spawnPoint;
    
    public bool         inOperation = false; //������ �̹� ���������� Ȯ���ϴ� ���

    private List<Unit> unitList;
    public List<Unit> UnitList => unitList;

    private void Awake()
    {
        unitList = new List<Unit>();
    }
    private void Start()
    {
        WayPointScan(); //�跰�� �����Ǹ� ���� ����� waypoint�� Ž��
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

        foreach (GameObject wayPoint in wayPoints) //��� wayPoint�� ������� ���� ����� wayPoint�� Ž��
        {
            float distance = Vector3.Distance(towerTransform.position, wayPoint.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                this.spawnPoint = wayPoint.transform; //���� ����� waypoint�� spawnPoint�� ����
            }
        }
    }

    public void UnitChoice(int unit)  //���õ� ������ int�� ���� �� ���ֻ��������� ��ȯ
    {
        spawnUnit       = this.unit[unit];
        inOperation     = true;
        StartCoroutine("SpawnUnit");
    }

    private IEnumerator SpawnUnit()
    {
        while (true)
        {
            GameObject  clone   = Instantiate(spawnUnit);       //���� ����
            Unit        unit    = clone.GetComponent<Unit>();   
            unit.Setup(wayPoints, spawnPoint);                  //���� ���� ����ó�� ����
            unitList.Add(unit); //unitList�� ��������
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
