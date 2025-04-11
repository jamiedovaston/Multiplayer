using Game.Services;
using Steamworks;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Tasks
{
    public class EndSteamServicesTask : GameTaskBase
    {
        public override IEnumerator ExecuteInternal(Action<string> OnAbortTasksWithError)
        {
            yield return PlayerServices.SessionLogout();

            if (SteamManager.Initialized)
            {
                Debug.Log("Shutting down Steam services...");
                try
                {
                    // Shutdown Steam API
                    SteamAPI.Shutdown();
                    Debug.Log("SteamAPI successfully shut down.");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error shutting down SteamAPI: {ex.Message}");
                    OnAbortTasksWithError?.Invoke($"Steam shutdown error: {ex.Message}");
                }
            }
            else
            {
                Debug.LogWarning("SteamManager not initialised; skipping shutdown.");
            }
        }
    }
}
