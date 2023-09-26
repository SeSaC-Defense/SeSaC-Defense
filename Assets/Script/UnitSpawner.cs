using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class UnitSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject[] unitPrefabs;
    [SerializeField]
    private GameObject unitHealthBarPrefab;

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
            SpawnUnitServerRpc(NetworkManager.Singleton.LocalClientId, unitTypeIx, transform.position);

            yield return new WaitForSeconds(unitSpawnInterval);
        }
    }

    [ServerRpc]
    private void SpawnUnitServerRpc(ulong clientId, int unitTypeIx, Vector3 spawnPosition)
    {
        GameObject unitPrefab = unitPrefabs[unitTypeIx];
        GameObject clone = Instantiate(unitPrefab, spawnPosition, Quaternion.identity);

        Unit unit = clone.GetComponent<Unit>();

        clone.GetComponent<NetworkObject>().SpawnWithOwnership(clientId);

        unit.SetupClientRpc();

        ulong unitNetworkId = clone.GetComponent<NetworkObject>().NetworkObjectId;
        CreateHealthBarClientRpc(clientId, unitNetworkId);
    }

    [ClientRpc]
    private void CreateHealthBarClientRpc(ulong clientId, ulong unitTransformId)
    {
        NetworkObject unitObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[unitTransformId];
        Transform transform = unitObject.transform;
        GameObject unitHealthBar = Instantiate(unitHealthBarPrefab);
        unitHealthBar.GetComponent<UnitHealthBar>().Setup(clientId, transform);
    }

}
