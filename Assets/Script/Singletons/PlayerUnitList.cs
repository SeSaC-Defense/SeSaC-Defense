using Pattern;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerUnitList : NetworkSingleton<PlayerUnitList>
{
    //[SerializeField]
    //private UnitProgressBar unitProgressBar;

    private List<Transform>[] unitTransformLists;
    public List<Transform>[] UnitTransformLists
    {
        get
        {
            return unitTransformLists;
        }
    }

    private void Start()
    {
        unitTransformLists = new List<Transform>[2];
        unitTransformLists[0] = new List<Transform>();
        unitTransformLists[1] = new List<Transform>();
    }

    public IReadOnlyList<Transform> GetEnemyUnitList(int playerNo)
    {
        return unitTransformLists[playerNo == 0 ? 1 : 0];
    }

    [ClientRpc]
    public void AddUnitClientRpc(ulong transformId)
    {
        NetworkObject networkObject = NetworkManager.SpawnManager.SpawnedObjects[transformId];
        Transform unitTransform = networkObject.transform;

        if (networkObject.IsOwnedByServer)
        {
            unitTransformLists[0].Add(unitTransform);
        }
        else
        {
            unitTransformLists[1].Add(unitTransform);
        }
    }

    [ClientRpc]
    public void RemoveUnitClientRpc(ulong transformId)
    {
        NetworkObject networkObject = NetworkManager.SpawnManager.SpawnedObjects[transformId];
        Transform unitTransform = networkObject.transform;

        if (networkObject.IsOwnedByServer)
        {
            unitTransformLists[0].Remove(unitTransform);
        }
        else
        {
            unitTransformLists[1].Remove(unitTransform);
        }
    }

}
