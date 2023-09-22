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
    [SerializeField]
    private GameObject[] towerPrefab;
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private PlayerGold playerGold;
    [SerializeField]
    private int towerBuildGold = 5;

    public float TowerBuildGold
    {
        get;
        set;
    }

    public TowerType TowerChosen { get; set; }


    public void SpawnTower(int i)
    {
        int ix = (int)TowerChosen;
        Transform tileTransform = ObjectDetector.Instance.Tile[i];
        Tile tile = tileTransform.GetComponent<Tile>();
        if (playerGold.CurrentGold < towerBuildGold)
        {
            return;
        }
        if (tile.IsBuildTower == true) return;

        playerGold.CurrentGold -= towerBuildGold;

        tile.IsBuildTower = true;
        GameObject clone = Instantiate(towerPrefab[ix], tileTransform);
        if (ix == 0)
        {
            clone.GetComponent<UnitSpawner>().Setup(wayPoints);
            return;
        }
        clone.GetComponent<TowerWeapon>().Setup();
    }

    public void SetTowerType(int towerTypeInInt)
    {
        TowerChosen = (TowerType)towerTypeInInt;
    }
}
