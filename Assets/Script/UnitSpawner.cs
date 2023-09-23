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
    
    private PlayerUnitList playerUnitList; //생성하는 유닛을 list로 보관
    private GameObject spawnUnit; //생성되는 유닛
    private Transform spawnPoint; //스폰될 위치
    private Transform[] waypoints; //이동하게될 waypoint

    public bool InOperation
    {
        set; get;
    }

    private void Awake()
    {
        InOperation = false; //배럭가동전
    }

    private void Start()
    {
        WayPointScan(); //waypoint중 가장 가까운것 탐색
    }

    public void Setup(Transform[] waypoints, PlayerUnitList playerUnitList) //배럭 설치와 동시에 설정되는 정보들
    {
        this.playerUnitList = playerUnitList;
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

    public void UnitChoice(int unit) // 생성할 유닛을 선택한다
    {
        unitData = unitDatas[unit]; //unitdata에서 index로 정보선택
        spawnUnit = unitPrefab; 
        InOperation = true; //유닛가동시작(중복 가동을 막기위한 bool값)
        StartCoroutine("SpawnUnit"); 
    }

    private IEnumerator SpawnUnit()
    {
        while (true)
        {
            GameObject clone = Instantiate(spawnUnit); //유닛 생성
            Unit unit = clone.GetComponent<Unit>();
            unit.SetUp(waypoints, spawnPoint, playerUnitList, unitData); // 생성 유닛 정보처리 함수
            clone.GetComponent<UnitHP>().SetUp(unitData); //유닛의 HP 정보처리 함수
            playerUnitList.UnitList.Add(unit); //생성되는 유닛을 list에 담아준다
            yield return new WaitForSeconds(spawnTime); //spawnTime 만큼 기다린 후 다시 sapwnUnit함수 시작
        }
    }
}
