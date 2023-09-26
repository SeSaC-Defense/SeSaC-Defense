using Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameLifecycle : NetworkSingleton<GameLifecycle>
{
    [SerializeField]
    GameObject playerPrefab;

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
        NetworkManager.Singleton.OnServerStopped += OnServerStopped;
    }

    public void OnClientConnected(ulong clientId)
    {
        if (IsHost
            && NetworkManager.ConnectedClientsList.Count == 2)
        {
            RelayManager.Instance.OnStartGameHost();
            StartGame();
        }
    }

    public void OnClientDisconnect(ulong clientId)
    {
        if (IsHost)
        {
            // TODO: EndGame();
        }
    }

    public void OnServerStopped(bool IsServerAHost)
    {
        if (!IsHost)
        {
            // TODO: EndGame();
        }
    }

    private void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        for (int i = 1; i > 0; i--)
        {
            CountDownClientRpc(i);
            yield return new WaitForSeconds(1f);
        }

        IReadOnlyList<NetworkClient> list = NetworkManager.ConnectedClientsList;

        foreach (var client in list)
        {
            GameObject player = Instantiate(playerPrefab);
            ulong clientId = client.ClientId;

            player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
            player.GetComponent<Player>().Setup(GetEnemyId(clientId));
        }

        yield return new WaitForSeconds(1f);

        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            client.PlayerObject.GetComponent<Player>().SetCameraClientRpc();
        }

        ShowGameUIClientRpc();
    }

    [ClientRpc]
    private void CountDownClientRpc(int count)
    {
        Debug.Log($"Game starting in {count}...");
    }

    [ClientRpc]
    private void ShowGameUIClientRpc()
    {
        GameObject.Find("Canvas").transform.Find("CanvasGame").gameObject.SetActive(true);
    }

    private ulong GetEnemyId(ulong clientId)
    {
        IReadOnlyList<NetworkClient> list = NetworkManager.ConnectedClientsList;

        foreach (var client in list)
        {
            if (client.ClientId != clientId)
            {
                return client.ClientId;
            }
        }

        return 0;
    }
}
