using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Unit : NetworkBehaviour
{
    [SerializeField]
    private UnitTemplate template;

    private int playerNo = -1;
    private int currentWaypointIndex = -1;
    private Movement2D movement2D;

    public float SpawnInterval => template.spawnInterval;
    private int Direction => playerNo == 0 ? 1 : -1;
    private float UnitMaxHealth => template.unitData[playerNo].maxHealth;
    private float UnitCurrentHealth { get; set; }
    private Transform[] Waypoints => TileMapWaypoint.Instance.Waypoints;

    public void Setup(int playerNo, Transform barrackTransform)
    {
        this.playerNo = playerNo;
        this.movement2D = GetComponent<Movement2D>();
        this.UnitCurrentHealth = UnitMaxHealth;
        this.transform.position = barrackTransform.position;

        this.currentWaypointIndex = FindNearestWaypointIndex();
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = template.unitData[playerNo].sprite;

        Transform nearestWaypointTransform = FindNearestWaypointTransform();
        transform.position = GetNearestSpawnPosition(nearestWaypointTransform);
        
        SpawnUnitServerRpc();

        StartCoroutine("OnMove");
    }

    [ServerRpc]
    private void SpawnUnitServerRpc()
    {
        SpawnUnitClientRpc();
    }

    [ClientRpc]
    private void SpawnUnitClientRpc()
    {
        PlayerUnitList.Instance.AddUnit(this);
    }

    private Vector3 GetNearestSpawnPosition(Transform nearestWaypoint)
    {
        Vector3 spawnPosition = nearestWaypoint.position;
        float deltaX = Mathf.Abs(nearestWaypoint.position.x - transform.position.x);
        float deltaY = Mathf.Abs(nearestWaypoint.position.y - transform.position.y);

        if (deltaX > deltaY)
        {
            spawnPosition.y = transform.position.y;
        }
        else
        {
            spawnPosition.x = transform.position.x;
        }

        return spawnPosition;
    }

    private IEnumerator OnMove()
    {
        while (true)
        {
            Debug.Log(currentWaypointIndex);
            float distance = Vector3.Distance(transform.position, Waypoints[currentWaypointIndex].position);
            
            if (distance < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }

    private int FindNearestWaypointIndex()
    {
        int nearestWaypointIndex = -1;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < Waypoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, Waypoints[i].position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestWaypointIndex = i;
            }
        }

        return nearestWaypointIndex;
    }

    private Transform FindNearestWaypointTransform()
    {
        Transform nearestWaypointTransform = null;
        float closestDistance = float.MaxValue;

        foreach (Transform waypointTransform in Waypoints)
        {
            float distance = Vector3.Distance(transform.position, waypointTransform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestWaypointTransform = waypointTransform;
            }
        }

        return nearestWaypointTransform;
    }

    private void NextMoveTo()
    {
        int waypointEndIndex = playerNo == 0 ? Waypoints.Length - 1 : 0;
        
        if ((waypointEndIndex - currentWaypointIndex) * Direction > 0)
        {
            transform.position = Waypoints[currentWaypointIndex].position;
            currentWaypointIndex += Direction;
            Vector3 direction = (Waypoints[currentWaypointIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            Arrive();
        }
    }

    public void TakeDamage(float damage)
    {
        TakeDamageServerRpc(damage);
    }

    [ServerRpc]
    private void TakeDamageServerRpc(float damage)
    {
        TakeDamageClientRpc(damage);
    }

    [ClientRpc]
    private void TakeDamageClientRpc(float damage)
    {
        if (damage < 0) return;

        UnitCurrentHealth -= damage;

        if (UnitCurrentHealth <= 0
            && NetworkManager.Singleton.IsHost)
        {
            KillServerRpc();
        }
    }
    
    public void Arrive()
    {
        if (!NetworkManager.Singleton.IsHost)
            return;

        NetworkObject networkObject = this.GetComponent<NetworkObject>();
        
        Player server = NetworkManager.LocalClient.PlayerObject.GetComponent<Player>();
        
        if (networkObject.IsOwnedByServer)
        {
            Player client = NetworkManager.Singleton.ConnectedClients[server.EnemyId].PlayerObject.GetComponent<Player>();
            client.TakeDamage(1);
        }
        else
        {
            server.TakeDamage(1);
        }

        KillServerRpc();
    }

    [ServerRpc]
    private void KillServerRpc()
    {
        NetworkObject networkObject = GetComponent<NetworkObject>();
        PlayerUnitList.Instance.RemoveUnit(this);
        networkObject.Despawn(true);
    }
}
