using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField]
    private float damage;

    private Movement2D movement2D;

    public void Setup(Vector3 targetPosition)
    {
        this.movement2D = GetComponent<Movement2D>();

        Vector3 direction = (targetPosition - transform.position).normalized;
        movement2D.MoveTo(direction);
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
