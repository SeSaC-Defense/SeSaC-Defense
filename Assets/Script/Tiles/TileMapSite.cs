using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapSite : MonoBehaviour
{
    [SerializeField]
    private Transform[] tiles;
    [SerializeField]
    private int playerNo;

    public int PlayerNo => playerNo;
}
