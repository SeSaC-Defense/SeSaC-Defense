using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private int             wayPointCount;      //�̵� ��� ����
    private Transform[]     wayPointsList;      //�̵� ��� ����
    private int             currentIndex = 0;   //���� ��ǥ���� �ε���
    private Movement2D      movement2D;         //������Ʈ �̵� ����

    private void Awake()
    {
        GameObject waypointlistobject   = GameObject.Find("WayPointList");
        //wayPointsList                   = waypointlistobject.GetComponent<WayPointList>().wayPoints;
    }

    public void SetUp(Transform loadPlace)
    {
        movement2D      = GetComponent<Movement2D>();
        wayPointCount   = wayPointsList.Length; //���� �̵� ��� waypoint ���� ����

        for (int i = 0; i < wayPointCount; i++)
        {
            if (wayPointsList[i] == loadPlace)
            {
                currentIndex = i;
                break; // ��ġ�ϴ� �ε����� ã���� ���� ����
            }
        }

        transform.position = loadPlace.position;            //������ ù ��ġ�� �����ϴ� waypoint 

        StartCoroutine("OnMove");                           //�� �̵�/��ǥ���� ���� �ڷ�ƾ ����
    }

    private IEnumerator OnMove()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, wayPointsList[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
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
            transform.position = wayPointsList[currentIndex].position;
            currentIndex ++;
            Vector3 direction = (wayPointsList[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            Destroy(gameObject); //���� waypoint�� �������������� ���� ����
        }
    }
}
