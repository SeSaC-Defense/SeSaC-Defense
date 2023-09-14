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
    public TowerType TowerChosen { get; set; }

    public void SpawnTower(Transform tileTransform)
    {
        int ix = (int)TowerChosen;
        Tile tile = tileTransform.GetComponent<Tile>();

        if (tile.IsBuildTower == true) return;
        tile.IsBuildTower = true;
        GameObject clone = Instantiate(towerPrefab[ix], tileTransform);
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
    }

    public void SetTowerType(int towerTypeInInt)
    {
        TowerChosen = (TowerType)towerTypeInInt;
    }
}
