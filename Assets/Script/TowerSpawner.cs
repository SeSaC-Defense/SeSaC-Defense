using Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public enum TowerType
{
    Barrack,
    Magician1,
    Magician2,
    Magician3,
    Magician4
}

public class TowerSpawner : NetworkSingleton<TowerSpawner>
{
    [SerializeField]
    private GameObject[] towerPrefabs;
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private int towerBuildGold = 5;

    private PlayerGold playerGold => NetworkManager.LocalClient.PlayerObject.GetComponent<PlayerGold>();

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
        if (tile.HasBuilding == true) return;

        playerGold.CurrentGold -= towerBuildGold;

        tile.HasBuilding = true;
        if (ix == 0)
        {
            SpawnBarrack(NetworkManager.Singleton.LocalClientId, tileTransform);
            return;
        }
        SpawnTower(NetworkManager.Singleton.LocalClientId, tileTransform);
    }

    public void SetTowerType(int towerTypeInInt)
    {
        TowerChosen = (TowerType)towerTypeInInt;
    }

    private void SpawnBarrack(ulong clientId, Transform tileTransform)
    {
        GameObject clone = Instantiate(towerPrefabs[0], tileTransform);

        clone.GetComponent<UnitSpawner>().Setup(clientId, clientId == NetworkManager.ServerClientId ? 0 : 1);
    }

    private void SpawnTower(ulong clientId, Transform tileTransform)
    {
        int ix = (int)TowerChosen;

        GameObject clone = Instantiate(towerPrefabs[ix], tileTransform);

        clone.GetComponent<TowerWeapon>().Setup(clientId, clientId == NetworkManager.ServerClientId ? 0 : 1);

    }
}
