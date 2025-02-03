using Game.Services;
using Game.Tasks;
using Steamworks;
using System.Threading.Tasks;
using UnityEngine;
using System;
using System.Collections;

public class GameTask_CreateSession : GameTaskBase
{
    private bool taskComplete = false;
    Action<string> OnAbortWithErrror;

    public override IEnumerator ExecuteInternal(Action<string> OnAbortTasksWithError)
    {
        OnAbortWithErrror = OnAbortTasksWithError;

        Callback<GetTicketForWebApiResponse_t> m_GetSessionTicketForWebApi = Callback<GetTicketForWebApiResponse_t>.Create(OnAuthTicketRetrieved);
        SteamUser.GetAuthTicketForWebApi("jamdov");

        // Wait until the task completes
        yield return new WaitUntil(() => taskComplete);

        if (taskComplete)
        {
            Debug.Log("Session successfully authorized and Steam session ID retrieved.");
        }
        else
        {
            OnAbortTasksWithError?.Invoke("Failed to authorize session or retrieve Steam ID.");
        }
    }

    private void OnAuthTicketRetrieved(GetTicketForWebApiResponse_t callback) => StartCoroutine(C_AuthorizeSession(callback));

    private IEnumerator C_AuthorizeSession(GetTicketForWebApiResponse_t callback)
    {
        Debug.Log("Callback received!");

        yield return PlayerServices.AuthorizeSessionWithSteamAuthTicket(callback.m_rgubTicket);

        yield return PlayerServices.GetSessionSteamID(
            (csteamIdCallback) =>
            {
                // Successful session SteamID retrieval
                Debug.Log("Session Steam ID retrieved.");
                taskComplete = true;
            },
            (error) =>
            {
                OnAbortWithErrror?.Invoke($"Error retrieving session SteamID: {error}");
            }
        );
    }
}
