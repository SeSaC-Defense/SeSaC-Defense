using Unity.Netcode;
using UnityEngine;

public class Tile : NetworkBehaviour
{
    public NetworkVariable<bool> hasBuilding;
    public bool HasBuilding { get => hasBuilding.Value; set => hasBuilding.Value = value; }
    public int PlayerNo => transform.parent.GetComponent<TileMapSite>().PlayerNo;

    private void Awake()
    {
        hasBuilding = new NetworkVariable<bool>(false);
        hasBuilding.Value = false;
    }
}