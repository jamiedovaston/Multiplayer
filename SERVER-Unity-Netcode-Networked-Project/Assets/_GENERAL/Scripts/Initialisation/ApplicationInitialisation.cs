using Steamworks;
using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationInitialisation : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    private static void Initialise()
    {
        Console.WriteLine($"Server Build Commenced.");
        Instantiate(Resources.Load<GameObject>("Networking/ServerGO"));
    }
}