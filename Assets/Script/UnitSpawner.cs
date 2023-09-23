using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    private int playerNumber = 1;
    public int PlayerNumber => playerNumber;

    [SerializeField]
    private List<UnitData> unitDatas = new List<UnitData>();
    private UnitData unitData;
    [SerializeField]
    private GameObject unitPrefab;

    [SerializeField]
    private float spawnTime;
    
    private PlayerUnitList playerUnitList; //�����ϴ� ������ list�� ����
    private GameObject spawnUnit; //�����Ǵ� ����
    private Transform spawnPoint; //������ ��ġ
    private Transform[] waypoints; //�̵��ϰԵ� waypoint

    public bool InOperation
    {
        set; get;
    }

    private void Awake()
    {
        InOperation = false; //�跰������
    }

    private void Start()
    {
        WayPointScan(); //waypoint�� ���� ������ Ž��
    }

    public void Setup(Transform[] waypoints, PlayerUnitList playerUnitList) //�跰 ��ġ�� ���ÿ� �����Ǵ� ������
    {
        this.playerUnitList = playerUnitList;
        this.waypoints = waypoints;
    }

    public void WayPointScan() //waypoint�� ���� ������ Ž��
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
                this.spawnPoint = wayPoint.transform; //���� ����� waypoint ����
            }
        }
    }

    public void UnitChoice(int unit) // ������ ������ �����Ѵ�
    {
        unitData = unitDatas[unit]; //unitdata���� index�� ��������
        spawnUnit = unitPrefab; 
        InOperation = true; //���ְ�������(�ߺ� ������ �������� bool��)
        StartCoroutine("SpawnUnit"); 
    }

    private IEnumerator SpawnUnit()
    {
        while (true)
        {
            GameObject clone = Instantiate(spawnUnit); //���� ����
            Unit unit = clone.GetComponent<Unit>();
            unit.SetUp(waypoints, spawnPoint, playerUnitList, unitData); // ���� ���� ����ó�� �Լ�
            clone.GetComponent<UnitHP>().SetUp(unitData); //������ HP ����ó�� �Լ�
            playerUnitList.UnitList.Add(unit); //�����Ǵ� ������ list�� ����ش�
            yield return new WaitForSeconds(spawnTime); //spawnTime ��ŭ ��ٸ� �� �ٽ� sapwnUnit�Լ� ����
        }
    }
}
