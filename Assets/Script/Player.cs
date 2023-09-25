using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public delegate void PlayerCameraChanged(Camera camera);
    public static event PlayerCameraChanged OnPlayerCameraChange;

    //[SerializeField]
    //private UICount hpText;

    private Camera cameraPlayerBase;
    private Camera cameraEnemyBase;
    private ulong enemyId;
    private int maxHP = 10;
    private int currentHP;
    
    public int MaxHP => maxHP;
    public int CurrentHP => currentHP;
    public int PlayerNo => IsOwnedByServer ? 0 : 1;
    public ulong EnemyId => enemyId;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void Setup(ulong enemyId)
    {
        this.enemyId = enemyId;
    }

    [ClientRpc]
    public void SetCameraClientRpc()
    {
        if (IsHost)
        {
            cameraPlayerBase = GameObject.Find("CameraPlayer0").GetComponent<Camera>();
            cameraEnemyBase = GameObject.Find("CameraPlayer1").GetComponent<Camera>();
        }
        else
        {
            cameraPlayerBase = GameObject.Find("CameraPlayer1").GetComponent<Camera>();
            cameraEnemyBase = GameObject.Find("CameraPlayer0").GetComponent<Camera>();
        }

        SwitchCameraToPlayerBase();
    }

    public void TakeDamage(int damage)
    {
        TakeDamageServerRpc(damage);
    }

    [ServerRpc]
    private void TakeDamageServerRpc(int damage)
    {
        TakeDamageClientRpc(damage);
    }

    [ClientRpc]
    private void TakeDamageClientRpc(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            print("die");
            //³¡
        }
    }

    public void SwitchCameraToPlayerBase()
    {
        cameraPlayerBase.enabled = true;
        cameraEnemyBase.enabled = false;
        OnPlayerCameraChange?.Invoke(cameraPlayerBase);
    }

    public void SwitchCameraToEnemyBase()
    {
        cameraPlayerBase.enabled = false;
        cameraEnemyBase.enabled = true;
        OnPlayerCameraChange?.Invoke(cameraEnemyBase);
    }
}
