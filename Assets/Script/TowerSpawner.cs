using Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Barrack,
    Magician1,
    Magician2,
    Magician3,
    Magician4
}

public class TowerSpawner : Singleton<TowerSpawner>
{
    [SerializeField] private GameObject[]   towerPrefab;
    [SerializeField] private EnemySpawner   enemySpawner;
    [SerializeField] private Transform[]    wayPoints;
    [SerializeField] private PlayerGold     playerGold;
    [SerializeField] private int            towerBuildGold = 5;

    public TowerType TowerChosen { get; set; }

    public void SpawnTower(Transform tileTransform)
    {
        print("鸥况积己");
        int ix = (int)TowerChosen;
        Tile tile = tileTransform.GetComponent<Tile>();
        if (playerGold.CurrentGold < towerBuildGold)
        {
            Debug.Log("榜靛何练");
            return;
        }

        if (tile.IsBuildTower == true) return;

        playerGold.CurrentGold -= towerBuildGold;
        print(playerGold.CurrentGold);

        tile.IsBuildTower = true;
        GameObject clone = Instantiate(towerPrefab[ix], tileTransform);
        if (ix == 0)
        {
            clone.GetComponent<UnitSpawner>().Setup(wayPoints);
            return;
        }
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
    }

    public void SetTowerType(int towerTypeInInt)
    {
        TowerChosen = (TowerType)towerTypeInInt;
    }
}
