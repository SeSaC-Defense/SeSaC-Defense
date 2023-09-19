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

    private float currentGold; //현재 소유한 골드
    private float towerBuildGold; //건설에 필요한 골드
    private Transform choiceTile; //건설될 건물 위치
    private int towerPrefabIndex = 1; //설치될 건물 index 번호
    private Transform saveTile; //Enemy가 넘어선 안될 타일
    private int enemyNumbers; //적의 수
    private bool attack = false; //배럭을 지을지, 타워를 지을지

    private void Start()
    {
        Warning(); //첫 시작은 타워 1개로 시작
        saveTile = tile[5];
        StartCoroutine(Build());
    }

    private IEnumerator Build() //설치 반복문
    {
        while(true)
        {
            currentGold = playerGold.CurrentGold; //현재 소유 골드
            towerBuildGold = towerSpawner.TowerBuildGold; //타워 설치시 필요 골드

            if (currentGold >= towerBuildGold)
            {
                enemyNumbers = enemySpawner.EnemyList.Count;
                for (int i = 0; i < enemyNumbers; i++)
                {
                    Transform enemypos = enemySpawner.EnemyList[i].transform;
                    if (enemypos.transform.position.x <= saveTile.transform.position.x) //적의 위치가 savetile보다 왼쪽이라면..
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

    public void Safety() //배럭 설치 함수
    {
        this.choiceTile = tile[Random.Range(0, tile.Length)];
        towerPrefabIndex = 0;
        towerSpawner.SetTowerType(towerPrefabIndex);
        towerSpawner.SpawnTower(choiceTile);
        //배럭 유닛 생성 추가 조작
    }

    public void Warning() //타워 설치 함수
    {
        this.choiceTile = tile[Random.Range(0, tile.Length)];
        towerPrefabIndex = 1; //타워 종류 생길 경우 추가 조작
        towerSpawner.SetTowerType(towerPrefabIndex);
        towerSpawner.SpawnTower(choiceTile);
    }
}
