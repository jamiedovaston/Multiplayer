using Game.Services;
using Game.Tasks;
using Steamworks;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class GameTask_SteamServicesVerification : GameTaskBase
{
    public override IEnumerator ExecuteInternal(Action<string> OnAbortTasksWithError)
    {
        if (!SteamAPI.Init())
        {
            Debug.LogError("SteamAPI_Init() failed. Steam API not available.");
            yield return null;
        }

        if (!SteamUser.BLoggedOn())
        {
            Debug.LogError("Steam user is not logged in!");
            yield return null;
        }

        if (!SteamManager.Initialized)
        {
            Debug.LogError("Steam API not initialised!");
            yield return null;
        }
    }
}
