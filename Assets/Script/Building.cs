using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Building : NetworkBehaviour
{
    [ServerRpc]
    public void DestructServerRpc()
    {
        Tile tile = transform.parent.GetComponent<Tile>();
        tile.HasBuilding = false;
        GetComponent<NetworkObject>().Despawn(true);
    }
}
