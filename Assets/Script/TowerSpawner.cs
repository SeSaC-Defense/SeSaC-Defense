using Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public enum TowerType
{
    Barrack,
    Magician1,
    Magician2,
    Magician3,
    Magician4
}

public class TowerSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject[] towerPrefabs;
    [SerializeField]
    private int towerBuildGold = 5;

    public Transform[] WayPoints => TileMapWaypoint.Instance.Waypoints;

    private PlayerGold playerGold => NetworkManager.LocalClient.PlayerObject.GetComponent<PlayerGold>();

    public float TowerBuildGold
    {
        get;
        set;
    }

    public TowerType TowerChosen { get; set; }

    public void SpawnTower(Transform tileTransform)
    {
        Tile tile = tileTransform.GetComponent<Tile>();
        if (playerGold.CurrentGold < towerBuildGold)
            return;

        if (tile.HasBuilding == true)
            return;

        playerGold.CurrentGold -= towerBuildGold;

        GameUIManager uiManager = GameObject.Find("Canvas").transform.Find("CanvasGame").GetComponent<GameUIManager>();
        uiManager.UpdateGoldText();

        ulong tileObjectId = tileTransform.GetComponent<NetworkTransform>().NetworkObjectId;

        SetTileHasBuildingServerRpc(tileObjectId);

        int ix = (int)TowerChosen;

        if (ix == 0)
        {
            SpawnBarrackServerRpc(NetworkManager.Singleton.LocalClientId, tileObjectId);
            return;
        }
        SpawnTowerServerRpc(NetworkManager.Singleton.LocalClientId, tileObjectId, ix);
    }

    public void SetTowerType(int towerTypeInInt)
    {
        TowerChosen = (TowerType)towerTypeInInt;
    }

    [ServerRpc]
    private void SetTileHasBuildingServerRpc(ulong tileObjectId)
    {
        Transform tileTransform = NetworkManager.SpawnManager.SpawnedObjects[tileObjectId].transform;
        Tile tile = tileTransform.GetComponent<Tile>();
        tile.HasBuilding = true;
    }



    [ServerRpc]
    private void SpawnBarrackServerRpc(ulong clientId, ulong tileObjectId)
    {
        Transform tileTransform = NetworkManager.SpawnManager.SpawnedObjects[tileObjectId].transform;
        GameObject clone = Instantiate(towerPrefabs[0], tileTransform);

        NetworkObject networkObject = clone.GetComponent<NetworkObject>();
        networkObject.SpawnWithOwnership(clientId);
        networkObject.TrySetParent(tileTransform.GetComponent<NetworkObject>(), true);
    }

    [ServerRpc]
    private void SpawnTowerServerRpc(ulong clientId, ulong tileObjectId, int prefabIx)
    {
        Transform tileTransform = NetworkManager.SpawnManager.SpawnedObjects[tileObjectId].transform;
        GameObject clone = Instantiate(towerPrefabs[prefabIx], tileTransform);

        NetworkObject networkObject = clone.GetComponent<NetworkObject>();
        networkObject.SpawnWithOwnership(clientId);
        networkObject.TrySetParent(tileTransform.GetComponent<NetworkObject>(), true);

        clone.GetComponent<TowerWeapon>().Setup();
    }
}
