using Game.Services;
using Game.Tasks;
using Steamworks;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Tasks
{
    public class SteamServicesVerificationTask : GameTaskBase
    {
        public override IEnumerator ExecuteInternal(Action<string> OnAbortTasksWithError)
        {
            if (!SteamAPI.Init())
            {
                OnAbortTasksWithError?.Invoke("SteamAPI_Init() failed. Steam API not available.");
                yield break;
            }

            if (!SteamUser.BLoggedOn())
            {
                OnAbortTasksWithError?.Invoke("Steam user is not logged in!");
                yield break;
            }
        }
    }
}
