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
    private int playerNumber = 1;
    public int PlayerNumber => playerNumber;

    [SerializeField]
    private GameObject[] towerPrefab1;
    [SerializeField]
    private GameObject[] towerPrefab2;
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private PlayerGold playerGold;
    [SerializeField]
    private PlayerUnitList playerUnitList;
    [SerializeField]
    private int towerBuildGold = 5;
    
    private GameObject[] towerPrefab;

    private void Start()
    {
        if(playerNumber == 1)
        {
            towerPrefab = towerPrefab1;
        }
        else
        {
            towerPrefab = towerPrefab2;
        }
    }

    public float TowerBuildGold
    {
        get;
        set;
    }

    public TowerType TowerChosen { get; set; }


    public void SpawnTower(Transform tileTransform)
    {
        int ix = (int)TowerChosen;
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
            clone.GetComponent<UnitSpawner>().Setup(wayPoints, playerUnitList);
            return;
        }
        clone.GetComponent<TowerWeapon>().Setup(playerUnitList);
    }

    public void SetTowerType(int towerTypeInInt)
    {
        TowerChosen = (TowerType)towerTypeInInt;
    }
}
