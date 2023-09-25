using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UnitSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject[] unitPrefabs;

    private int playerNo = -1;
    private int unitTypeIx = -1;
    private float unitSpawnInterval = 1;

    public bool IsOperating
    {
        private set; get;
    }

    private void Awake()
    {
        IsOperating = false;
    }

    public void Setup(ulong clientId, int playerNo)
    {
        this.playerNo = playerNo;
        SpawnServerRpc(clientId);
    }

    [ServerRpc]
    private void SpawnServerRpc(ulong clientId)
    {
        NetworkObject networkObject = GetComponent<NetworkObject>();
        networkObject.SpawnWithOwnership(clientId);
    }

    public void UnitChoice(int unitTypeIx)
    {
        this.unitTypeIx = unitTypeIx;
        this.unitSpawnInterval = unitPrefabs[unitTypeIx].GetComponent<Unit>().SpawnInterval;
        IsOperating = true;
        StartCoroutine("SpawnUnit");
    }

    private IEnumerator SpawnUnit()
    {
        while (true)
        {
            SpawnUnitServerRpc(NetworkManager.Singleton.LocalClientId, unitTypeIx);

            yield return new WaitForSeconds(unitSpawnInterval);
        }
    }

    [ServerRpc]
    private void SpawnUnitServerRpc(ulong clientId, int unitTypeIx)
    {
        GameObject unitPrefab = unitPrefabs[unitTypeIx];
        GameObject clone = Instantiate(unitPrefab);

        Unit unit = clone.GetComponent<Unit>();

        clone.GetComponent<NetworkObject>().SpawnWithOwnership(clientId);

        unit.Setup(playerNo, transform);
    }
}
