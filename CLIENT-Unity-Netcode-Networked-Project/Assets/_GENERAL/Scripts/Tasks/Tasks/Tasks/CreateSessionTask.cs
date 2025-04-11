using Game.Services;
using Steamworks;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Tasks
{
    public class CreateSessionTask : GameTaskBase
    {
        private Action<string> OnAbortWithError;

        public override IEnumerator ExecuteInternal(Action<string> OnAbortTasksWithError)
        {
            OnAbortWithError = OnAbortTasksWithError;
            yield return StartCoroutine(HandleSessionCreation());
        }

        private IEnumerator HandleSessionCreation()
        {
            GetTicketForWebApiResponse_t ticketResponse = default;
            bool success = false;

            yield return StartCoroutine(GetSteamAuthTicketCoroutine(response =>
            {
                ticketResponse = response;
                success = true;
            },
            error =>
            {
                OnAbortWithError?.Invoke($"Failed to retrieve Steam auth ticket: {error}");
            }));

            if (!success) yield break;

            yield return PlayerServices.AuthorizeSessionWithSteamAuthTicket(ticketResponse.m_rgubTicket);
            Debug.Log("Session successfully authorized.");

            yield return PlayerServices.GetSessionSteamID(
                steamId => Debug.Log("Steam session ID retrieved."),
                error => OnAbortWithError?.Invoke($"Error retrieving Steam ID: {error}")
            );
        }

        private IEnumerator GetSteamAuthTicketCoroutine(Action<GetTicketForWebApiResponse_t> OnSuccess, Action<string> OnError)
        {
            bool completed = false;
            GetTicketForWebApiResponse_t ticketResponse = default;

            var callback = Callback<GetTicketForWebApiResponse_t>.Create(response =>
            {
                if (response.m_eResult == EResult.k_EResultOK)
                {
                    ticketResponse = response;
                    completed = true;
                }
                else
                {
                    OnError?.Invoke("Steam auth ticket request failed.");
                    completed = true;
                }
            });

            SteamUser.GetAuthTicketForWebApi("jamdov");

            yield return new WaitUntil(() => completed);

            if (completed && ticketResponse.m_eResult == EResult.k_EResultOK)
            {
                OnSuccess?.Invoke(ticketResponse);
            }
        }
    }
}
