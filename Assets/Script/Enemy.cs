using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UIElements;

public enum EnemyDestroyType { Kill = 0, Arrive }

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int gold = 1;

    private int wayPointCount;      //�̵� ��� ����
    private Transform[] EnemywayPoints;      //�̵� ��� ����
    private int currentIndex = 0;   //���� ��ǥ���� �ε���
    private Movement2D movement2D;         //������Ʈ �̵� ����

    public void SetUp(Transform[] wayPoints, Transform spawnPoint)
    {
        movement2D = GetComponent<Movement2D>();
        this.EnemywayPoints = wayPoints;
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
            if (Vector3.Distance(transform.position, EnemywayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if (currentIndex < wayPointCount - 1)
        {
            transform.position = EnemywayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (EnemywayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            gold = 0;
            OnDie(EnemyDestroyType.Arrive);
        }
    }

    public void OnDie(EnemyDestroyType type)
    {
        EnemyController.Instance.DestroyEnemy(type, this, gold);
    }
}
