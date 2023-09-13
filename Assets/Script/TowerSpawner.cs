using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[]   towerTamplate;
    [SerializeField] private Transform[]    wayPoints;
    [SerializeField] private EnemySpawner   enemySpawner;
                     private bool           isOnTowerButton = false;
                     private int            towerType;

    public void ReadyToSpawnTower(int type)
    {
        isOnTowerButton = true;
        towerType = type;
    }

    public void SpawnTower(Transform tileTransform)
    {
        if (isOnTowerButton == false)
        {
            return;
        }

        Tile tile = tileTransform.GetComponent<Tile>();

        if (tile.IsBuildTower == true) return;

        isOnTowerButton     = false;
        tile.IsBuildTower   = true;
        GameObject clone    = Instantiate(towerTamplate[towerType], tileTransform);
        //int값으로 건설된 건물이 Tower에 속하는지 Barrck에 속하는지 확인해서 setup함수 시작
        if (towerType >= 1)
        {
            clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
        }
        else if (towerType == 0)
        {
            clone.GetComponent<BarrackController>().Setup(wayPoints);
        }
    }
}
