using Game.Model;
using Game.Services;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using Steamworks;

public class ClientNetworkManager : NetworkManager
{
    private void Start()
    {
        UnityTransport transport = gameObject.AddComponent<UnityTransport>();
        NetworkConfig.NetworkTransport = transport;

        StartClient();
    }

    private async void OnDestroy()
    {
        await PlayerServices.SessionLogout();
        SteamAPI.Shutdown();
    }
}
