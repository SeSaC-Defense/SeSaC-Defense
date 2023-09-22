//using System.Collections;
//using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UI;

//public class AI : MonoBehaviour
//{
//    [SerializeField]
//    private TowerSpawner towerSpawner;
//    [SerializeField]
//    private Transform[] tiles;
//    [SerializeField]
//    private EnemySpawner enemySpawner;

//    private float currentGold; //현재 소유한 골드
//    private float towerBuildGold; //건설에 필요한 골드
//    private int hitIndex; //건설될 건물 위치
//    private int towerPrefabIndex = 1; //설치될 건물 index 번호
//    private Transform saveTile; //Enemy가 넘어선 안될 타일
//    private int enemyNumbers; //적의 수
//    private bool attack = false; //배럭을 지을지, 타워를 지을지


//    private void Start()
//    {
//        TileScan();
//        Build(1); //첫 시작은 타워 1개로 시작
//        saveTile = tiles[10];
//        StartCoroutine(Building());
//    }

//    private IEnumerator Building() //설치 반복문
//    {
//        while(true)
//        {
//            currentGold = playerGold.CurrentGold; //현재 소유 골드
//            towerBuildGold = towerSpawner.TowerBuildGold; //타워 설치시 필요 골드

//            if (currentGold >= towerBuildGold)
//            {
//                enemyNumbers = enemySpawner.EnemyList.Count;
//                for (int i = 0; i < enemyNumbers; i++)
//                {
//                    Transform enemypos = enemySpawner.EnemyList[i].transform;
//                    if (enemypos.transform.position.x <= saveTile.transform.position.x) //적의 위치가 savetile보다 왼쪽이라면..
//                    {
//                        attack = true;
//                        break;
//                    }
//                    else
//                    {
//                        attack = false;
//                    }
//                }
//                if(attack == true)
//                {
//                    TileScan();
//                    Build(1);
//                }
//                else if (attack == false)
//                {
//                    TileScan();
//                    Build(0);
//                    yield return new WaitForSeconds(1);
//                    UnitSpawn();
//                }
//                yield return new WaitForSeconds(1);
                
//            }
//        }
//    }

//    public void Build(int i) //배럭 설치 함수
//    {
//        towerPrefabIndex = i;
//        towerSpawner.SetTowerType(towerPrefabIndex);
//        towerSpawner.SpawnTower(hitIndex);
//    }

//    public void UnitSpawn()
//    {
//        foreach (Transform t in ObjectDetector.Instance.HitTransform) //배럭이 설치된 타일에서
//        {
//            if (t.CompareTag("Barrack")) //자식 오브젝트중 Tower 태그를 가진 오브젝트를 불러와서 UnitChoice함수 시작
//            {
//                t.GetComponent<UnitSpawner>().UnitChoice(Random.Range(0, 3));
//            }
//        } 
//    }

//    public void TileScan()
//    {
//        for( int i = 0; i < tiles.Length; i++)
//        {
//            int num = Random.Range(0, tiles.Length);
//            Transform t = tiles[num];
//            if (t.GetComponent<Tile>().IsBuildTower == false)
//            {
//                this.hitIndex = num;
//                return;
//            }
//        }
//    }
//}
