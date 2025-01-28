using Game.Model;
using Game.Services;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using Steamworks;

public class ClientNetworkManager : NetworkManager
{
    private async void Start()
    {
        UnityTransport transport = gameObject.AddComponent<UnityTransport>();
        NetworkConfig.NetworkTransport = transport;

        await PlayerServices.GetTime();

        if (!SteamAPI.Init())
        {
            Debug.LogError("SteamAPI_Init() failed. Steam API not available.");
            
            return;
        }

        CSteamID id;
        Callback<GetTicketForWebApiResponse_t> m_GetSessionTicketForWebApi = Callback<GetTicketForWebApiResponse_t>.Create(
            async (callback) =>
            {
                await PlayerServices.AuthorizeSessionWithSteamAuthTicket(callback.m_rgubTicket);
                id = await PlayerServices.GetSessionSteamID();
            });

        HAuthTicket ticket = SteamUser.GetAuthTicketForWebApi("jamdov");

        StartClient();
    }

    private async void OnDestroy()
    {
        await PlayerServices.SessionLogout();
        SteamAPI.Shutdown();
    }
}
