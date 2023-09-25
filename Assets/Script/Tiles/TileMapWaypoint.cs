using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapWaypoint : Singleton<TileMapWaypoint>
{
    [SerializeField]
    private Transform[] waypoints;

    public Transform[] Waypoints => waypoints;
}
