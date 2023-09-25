using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool HasBuilding;

    private void Awake()
    {
        HasBuilding = false;
    }

    public int PlayerNo => transform.parent.GetComponent<TileMapSite>().PlayerNo;
    
}