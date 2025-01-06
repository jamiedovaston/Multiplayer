using Game.Model;
using GameServices;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class ClientNetworkManager : NetworkManager
{
    async void Start()
    {
        UnityTransport transport = gameObject.AddComponent<UnityTransport>();
        NetworkConfig.NetworkTransport = transport;

        await PlayerServices.GetTime();
        Player player = await PlayerServices.GetPlayer("steam_id");

        Debug.Log($"Player retrieved: {player.Gamertag}");

        StartClient();
        Debug.Log($"Client Ready.");
    }
}
