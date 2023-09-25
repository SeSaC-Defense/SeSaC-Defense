using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameLifecycle : NetworkBehaviour
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
        Debug.Log(NetworkManager.ConnectedClientsList.Count);

        if (IsHost
            && NetworkManager.ConnectedClientsList.Count == 2)
        {
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
        if (IsClient)
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
        for (int i = 5; i > 0; i--)
        {
            Debug.Log($"Game starting in {i}...");
            yield return new WaitForSeconds(1f);
        }

        IReadOnlyList<NetworkClient> list = NetworkManager.ConnectedClientsList;


        foreach (var client in list)
        {
            GameObject player = Instantiate(playerPrefab);
            ulong clientId = client.ClientId;

            if (clientId == NetworkManager.ServerClientId)
            {
                player.GetComponent<Player>().Setup(0, GetEnemyId(clientId));
            }
            else
            {
                player.GetComponent<Player>().Setup(1, GetEnemyId(clientId));
            }

            player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        }
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
