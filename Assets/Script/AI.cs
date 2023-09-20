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
    [SerializeField]
    private UnitSpawner unitSpawner;

    private float currentGold; //���� ������ ���
    private float towerBuildGold; //�Ǽ��� �ʿ��� ���
    private Transform choiceTile; //�Ǽ��� �ǹ� ��ġ
    private int towerPrefabIndex = 1; //��ġ�� �ǹ� index ��ȣ
    private Transform saveTile; //Enemy�� �Ѿ �ȵ� Ÿ��
    private int enemyNumbers; //���� ��
    private bool attack = false; //�跰�� ������, Ÿ���� ������

    private void Start()
    {
        TileScan();
        Build(1); //ù ������ Ÿ�� 1���� ����
        saveTile = tile[10];
        print("saveTile" + saveTile.position);
        StartCoroutine(Building());
    }

    private IEnumerator Building() //��ġ �ݺ���
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
                    TileScan();
                    Build(1);
                }
                else if (attack == false)
                {
                    TileScan();
                    Build(0);
                }
                yield return new WaitForSeconds(1);
                
            }
        }
    }

    public void Build(int i) //�跰 ��ġ �Լ�
    {
        towerPrefabIndex = i;
        towerSpawner.SetTowerType(towerPrefabIndex);
        towerSpawner.SpawnTower(choiceTile);
        if (i == 0)
        {
            //��ġ�� Ÿ���� unitspawner�� �����ؾ��ϴµ� ��ĳ�ϳ�
            //unitSpawner.GetComponent<UnitSpawner>().UnitChoice(Random.Range(0, 3));
        }
    }

    public void TileScan()
    {
        for( int i = 0; i < tile.Length; i++)
        {
            Transform t = tile[Random.Range(0, tile.Length)];
            if (t.GetComponent<Tile>().IsBuildTower == false)
            {
                this.choiceTile = t;
                return;
            }
        }
    }
}
