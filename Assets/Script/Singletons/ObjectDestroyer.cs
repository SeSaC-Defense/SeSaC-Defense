using Pattern;
using Unity.Netcode;
using UnityEngine;

public class ObjectDestroyer : NetworkSingleton<ObjectDestroyer>
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!NetworkManager.Singleton.IsServer)
            return;
            
        switch (collision.tag)
        {
            case "Unit":
                collision.GetComponent<Unit>().Arrive();
                break;
            case "Projectile":
                collision.GetComponent<NetworkObject>().Despawn(true);
                break;
        }
    }
}
