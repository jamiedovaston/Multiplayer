using Game.Model;
using GameServices;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using Steamworks;
using UnityEngine.Networking;

public class ClientNetworkManager : NetworkManager
{
    private async void Start()
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

        CSteamID id;
        Callback<GetTicketForWebApiResponse_t> m_GetSessionTicketForWebApi = Callback<GetTicketForWebApiResponse_t>.Create(
            async (callback) =>
            {
                id = await PlayerServices.AuthorizeSessionWithSteamAuthTicket(callback.m_rgubTicket);
                Debug.Log($"ID = {id}");
            });

        // Request the auth ticket
        HAuthTicket ticket = SteamUser.GetAuthTicketForWebApi("jamdov");

        // Start the client
        StartClient();
    }

    void OnDestroy()
    {
        // Shutdown Steamworks when the application quits
        SteamAPI.Shutdown();
    }
}
