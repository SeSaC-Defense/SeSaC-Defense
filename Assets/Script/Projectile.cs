using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField]
    private float damage;

    private int playerNo;
    private Movement2D movement2D;

    public void Setup(ulong clientId, int playerNo, Transform target)
    {
        this.playerNo = playerNo;
        this.movement2D = GetComponent<Movement2D>();

        SpawnServerRpc(clientId);

        Vector3 direction = (target.position - transform.position).normalized;
        movement2D.MoveTo(direction);
    }

    [ServerRpc]
    private void SpawnServerRpc(ulong clientId)
    {
        NetworkObject networkObject = GetComponent<NetworkObject>();
        networkObject.SpawnWithOwnership(clientId);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Unit")) return;

        NetworkObject targetObject = collision.GetComponent<NetworkObject>();
        if (OwnerClientId == targetObject.OwnerClientId) return;

        collision.GetComponent<Unit>().TakeDamage(damage);
        HitTargetServerRpc();
    }

    [ServerRpc]
    private void HitTargetServerRpc()
    {
        GetComponent<NetworkObject>().Despawn(true);
    }
}
