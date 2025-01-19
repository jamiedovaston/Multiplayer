using Game.Model;
using GameServices;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using Steamworks;

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

        // Get the player's Steam ID
        CSteamID steamID = SteamUser.GetSteamID();
        string steamIDString = steamID.ToString();

        // Get the player data using the Steam ID
        await PlayerServices.CreatePlayer(steamIDString, $"Player {Random.Range(0, 1000)}");
        JSON.Player player = await PlayerServices.GetPlayer(steamIDString);

        // Use the player data as needed
        Debug.Log($"Player Gamertag: {player.gamertag}, Steam ID: {player.steam_id}");

        // Start the client
        StartClient();
    }

    void OnDestroy()
    {
        // Shutdown Steamworks when the application quits
        SteamAPI.Shutdown();
    }
}
