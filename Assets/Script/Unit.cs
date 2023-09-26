using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Unit : NetworkBehaviour
{
    [SerializeField]
    private UnitTemplate template;

    private int currentWaypointIndex = -1;
    private Movement2D movement2D;

    public float SpawnInterval => template.spawnInterval;
    public int PlayerNo => IsOwnedByServer ? 0 : 1;
    private int Direction => PlayerNo == 0 ? 1 : -1;
    public float UnitMaxHealth => template.maxHealth;
    private NetworkVariable<float> unitCurrentHealth;
    public float UnitCurrentHealth => unitCurrentHealth.Value;
    private Transform[] Waypoints => TileMapWaypoint.Instance.Waypoints;

    private void Awake()
    {
        unitCurrentHealth = new NetworkVariable<float>(UnitMaxHealth);
    }

    [ClientRpc]
    public void SetupClientRpc()
    {
        this.movement2D = GetComponent<Movement2D>();
        this.unitCurrentHealth.Value = UnitMaxHealth;

        this.currentWaypointIndex = FindNearestWaypointIndex();
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        int playerNo = IsOwnedByServer ? 0 : 1;
        spriteRenderer.sprite = template.playerSideData[playerNo].sprite;

        Transform nearestWaypointTransform = FindNearestWaypointTransform();
        transform.position = GetNearestSpawnPosition(nearestWaypointTransform);
        
        PlayerUnitList.Instance.AddUnit(this);

        StartCoroutine("OnMove");
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
        int waypointEndIndex = PlayerNo == 0 ? Waypoints.Length - 1 : 0;
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            TakeDamage(collision.GetComponent<Projectile>().Damage);
        }
    }
    
    public void TakeDamage(float damage)
    {
        Debug.Log($"TakeDamage: 1 {unitCurrentHealth.Value}");
        Debug.Log($"TakeDamage: 2 {Mathf.Max(0, damage)}");
        unitCurrentHealth.Value -= Mathf.Max(0, damage);

        Debug.Log($"TakeDamage: 3 {unitCurrentHealth.Value}");
        if (unitCurrentHealth.Value <= 0)
        {
            Kill();
        }
    }
    
    public void Arrive()
    {
        Kill();
    }

    private void Kill()
    {
        PlayerUnitList.Instance.RemoveUnit(this);

        if (IsOwner)
            KillServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void KillServerRpc()
    {
        GetComponent<NetworkObject>().Despawn(true);
    }
}
