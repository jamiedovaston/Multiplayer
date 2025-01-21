using Game.Model;
using GameServices;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using Steamworks;
using UnityEngine.Networking;

public class ClientNetworkManager : NetworkManager
{
    async void Start()
    {
        UnityTransport transport = gameObject.AddComponent<UnityTransport>();
        NetworkConfig.NetworkTransport = transport;

        await PlayerServices.GetTime();

        // Initialize Steamworks
        if (!SteamAPI.Init())
        {
            Debug.LogError("SteamAPI_Init() failed. Steam API not available.");
            return;
        }

        HAuthTicket ticket = SteamUser.GetAuthTicketForWebApi("jamie-dovaston");

        UnityWebRequest request = new UnityWebRequest();

        // Start the client
        StartClient();
    }

    void OnDestroy()
    {
        // Shutdown Steamworks when the application quits
        SteamAPI.Shutdown();
    }
}
