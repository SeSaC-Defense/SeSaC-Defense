using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public enum UnitDestroyType { Kill = 0, Arrive }

public class Unit : MonoBehaviour
{
    private int wayPointCount;      //�̵� ��� ����
    private Transform[] wayPoints;      //�̵� ��� ����
    private int currentIndex = 0;   //���� ��ǥ���� �ε���
    private Movement2D movement2D;         //������Ʈ �̵� ����

    public void SetUp(Transform[] wayPoints, Transform spawnPoint)
    {
        movement2D = GetComponent<Movement2D>();
        this.wayPoints = wayPoints;
        wayPointCount = wayPoints.Length;         //���� �̵� ��� waypoint ���� ����

        for (int i = 0; i < wayPointCount; i++)
        {
            if (wayPoints[i] == spawnPoint)
            {
                currentIndex = i;
                break;                              // ��ġ�ϴ� �ε����� ã���� ���� ����
            }
        }

        transform.position = spawnPoint.position;    //������ ù ��ġ�� �����ϴ� 

        StartCoroutine("OnMove");                    //�� �̵�/��ǥ���� ���� �ڷ�ƾ ����
    }

    private IEnumerator OnMove()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
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
            transform.position = wayPoints[currentIndex].position;
            currentIndex ++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            PlayerUnitList.Instance.UnitList.Remove(this);
            OnDie(); //���� waypoint�� �������������� ���� ����
        }
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }
}
