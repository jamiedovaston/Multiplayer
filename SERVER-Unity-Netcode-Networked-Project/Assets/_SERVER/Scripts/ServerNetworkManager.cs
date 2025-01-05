using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class ServerNetworkManager : NetworkManager
{
    private void OnEnable()
    {
        UnityTransport transport = gameObject.AddComponent<UnityTransport>();
        NetworkConfig.NetworkTransport = transport;

        OnServerStarted += ServerNetworkManager_OnServerStarted;
        OnClientConnectedCallback += ServerNetworkManager_OnClientConnectedCallback;
        OnClientDisconnectCallback += ServerNetworkManager_OnClientDisconnectCallback;

        StartServer();
    }

    private void ServerNetworkManager_OnClientConnectedCallback(ulong obj)
    {
        Console.WriteLine($"User ({obj}) connected to the server.");
    }

    private void ServerNetworkManager_OnClientDisconnectCallback(ulong obj)
    {
        Console.WriteLine($"User ({obj}) disconnected.");
    }

    private void ServerNetworkManager_OnServerStarted()
    {
        Console.WriteLine($"Server listening for connections...");
    }
}
