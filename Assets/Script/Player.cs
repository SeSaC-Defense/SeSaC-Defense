using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private ulong enemyId;
    private int maxHP = 10;
    private NetworkVariable<int> currentHP;
    
    public int MaxHP => maxHP;
    public int CurrentHP => currentHP.Value;
    public int PlayerNo => IsOwnedByServer ? 0 : 1;
    public ulong EnemyId => enemyId;

    private void Awake()
    {
        currentHP = new NetworkVariable<int>(maxHP);
    }

    public void Setup(ulong enemyId)
    {
        this.enemyId = enemyId;

        TileMapWaypoint tileMapWaypoint = TileMapWaypoint.Instance;

        Transform waypointFirst = tileMapWaypoint.Waypoints[0];
        Transform waypointLast = tileMapWaypoint.Waypoints[tileMapWaypoint.Waypoints.Length - 1];

        transform.position = PlayerNo == 0 ? waypointFirst.position : waypointLast.position;
    }

    [ClientRpc]
    public void SetCameraClientRpc()
    {
        if (IsHost)
        {
            CameraManager.Instance.SwitchCameraTo(0);
        }
        else
        {
            CameraManager.Instance.SwitchCameraTo(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Unit"))
        {
            TakeDamage(1);
        }
    }

    public void Start()
    {
        GameUIManager uiManager = GameObject.Find("Canvas").transform.Find("CanvasGame").GetComponent<GameUIManager>();

        if (IsOwnedByServer)
        {
            uiManager.Player0 = this;
        }
        else
        {
            uiManager.Player1 = this;
        }

        base.OnNetworkSpawn();
    }

    public void TakeDamage(int damage)
    {
        currentHP.Value -= damage;
    }
}
