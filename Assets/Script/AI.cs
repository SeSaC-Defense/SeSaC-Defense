using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    [SerializeField]
    private PlayerGold playerGold;
    [SerializeField]
    private TowerSpawner towerSpawner;
    [SerializeField]
    private Transform[] tile;
    [SerializeField]
    private EnemySpawner enemySpawner;

    private float currentGold; //���� ������ ���
    private float towerBuildGold; //�Ǽ��� �ʿ��� ���
    private Transform choiceTile; //�Ǽ��� �ǹ� ��ġ
    private int towerPrefabIndex = 1; //��ġ�� �ǹ� index ��ȣ
    private Transform saveTile; //Enemy�� �Ѿ �ȵ� Ÿ��
    private int enemyNumbers; //���� ��
    private bool attack = false; //�跰�� ������, Ÿ���� ������

    private void Start()
    {
        Warning(); //ù ������ Ÿ�� 1���� ����
        saveTile = tile[5];
        StartCoroutine(Build());
    }

    private IEnumerator Build() //��ġ �ݺ���
    {
        while(true)
        {
            currentGold = playerGold.CurrentGold; //���� ���� ���
            towerBuildGold = towerSpawner.TowerBuildGold; //Ÿ�� ��ġ�� �ʿ� ���

            if (currentGold >= towerBuildGold)
            {
                enemyNumbers = enemySpawner.EnemyList.Count;
                for (int i = 0; i < enemyNumbers; i++)
                {
                    Transform enemypos = enemySpawner.EnemyList[i].transform;
                    if (enemypos.transform.position.x <= saveTile.transform.position.x) //���� ��ġ�� savetile���� �����̶��..
                    {
                        attack = true;
                        break;
                    }
                    else
                    {
                        attack = false;
                    }
                }

                if(attack == true)
                {
                    Warning();
                }
                else if (attack == false)
                {
                    Safety();
                }
                yield return new WaitForSeconds(1);
                
            }
        }
    }

    public void Safety() //�跰 ��ġ �Լ�
    {
        this.choiceTile = tile[Random.Range(0, tile.Length)];
        towerPrefabIndex = 0;
        towerSpawner.SetTowerType(towerPrefabIndex);
        towerSpawner.SpawnTower(choiceTile);
        //�跰 ���� ���� �߰� ����
    }

    public void Warning() //Ÿ�� ��ġ �Լ�
    {
        this.choiceTile = tile[Random.Range(0, tile.Length)];
        towerPrefabIndex = 1; //Ÿ�� ���� ���� ��� �߰� ����
        towerSpawner.SetTowerType(towerPrefabIndex);
        towerSpawner.SpawnTower(choiceTile);
    }
}
