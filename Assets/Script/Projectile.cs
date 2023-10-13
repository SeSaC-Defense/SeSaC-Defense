using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField]
    private float damage;

    private Movement2D movement2D;

    public float Damage => damage;

    public void Setup(Vector3 targetPosition)
    {
        this.movement2D = GetComponent<Movement2D>();

        Vector3 direction = (targetPosition - transform.position).normalized;
        movement2D.MoveTo(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Unit"))
            return;

        NetworkObject projectileObject = GetComponent<NetworkObject>();
        NetworkObject targetObject = collision.GetComponent<NetworkObject>();

        if (projectileObject.OwnerClientId == targetObject.OwnerClientId)
            return;

        if (IsOwner)
            HitTargetServerRpc();
    }

    [ServerRpc]
    private void HitTargetServerRpc()
    {
        GetComponent<NetworkObject>().Despawn(true);
    }
}
