using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool HasBuilding
    {
        set; get;
    }

    private void Awake()
    {
        HasBuilding = false;
    }

    public int PlayerNo => transform.parent.GetComponent<TileMapSite>().PlayerNo;
    
}