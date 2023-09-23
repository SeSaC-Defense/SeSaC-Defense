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

    private Transform[] wayPoints;          //�̵� ��� ����
    private Transform spawnPoint;
    private Movement2D movement2D;          //������Ʈ �̵� ����
    private SpriteRenderer spriteRenderer;
    private int wayPointCount;              //�̵� ��� ����
    private int currentIndex = 0;           //���� ��ǥ���� �ε���
    
    public PlayerUnitList playerUnitList;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void SetUp(Transform[] wayPoints, Transform spawnPoint, PlayerUnitList playerUnitList, UnitData unitData) //���� ����ó��
    {
        this.unitData = unitData;
        this.playerUnitList = playerUnitList;
        spriteRenderer.sprite = unitData.Sprite;
        gameObject.name = unitData.UnitName;
        movement2D = GetComponent<Movement2D>();
        this.wayPoints = wayPoints;
        wayPointCount = wayPoints.Length; //���� �̵� ��� waypoint ���� ����

        for (int i = 0; i < wayPointCount; i++) //waypoints�߿��� spawnpoint�� ���� waypoint�� ã�Ƴ���
        {
            if (wayPoints[i] == spawnPoint)
            {
                currentIndex = i;
                break; // ��ġ�ϴ� �ε����� ã���� ���� ����
            }
        }
        transform.position = spawnPoint.position;    //������ ù ��ġ�� �����ϴ�
        StartCoroutine("OnMove");                    //�� �̵�/��ǥ���� ���� �ڷ�ƾ ����
    }

    private IEnumerator OnMove() //waypoint�� �̵��Ѵ�
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo(); //�����ϸ� ���� waypoint�� ã�´�
            }
            yield return null;
        }
    }

    private void NextMoveTo() //���� waypoint�� +1�� index�� ���� waypoint ã��
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
            playerUnitList.DestroyUnit(this); //���ٸ� ���� ���� ����
        }
    }
}
