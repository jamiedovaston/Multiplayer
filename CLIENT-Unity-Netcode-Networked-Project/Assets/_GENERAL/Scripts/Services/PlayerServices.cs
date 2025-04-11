using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Game.Model;
using System;
using Steamworks;
using Game.Services.Requests;
using System.Collections;
using Unity.VisualScripting;

namespace Game.Services
{
    public static class PlayerServices
    {
        // public static async Task GetTime()
        // {
        //     string response = await ServerRequest.GetRequest(URL("/tests/time"));
        //     Debug.Log(response);
        // }
        
        public static IEnumerator AuthorizeSessionWithSteamAuthTicket(byte[] _ticket)
        {
            UnityWebRequest request = new UnityWebRequest("https://unity-netcode-project-njs.xrdxno.easypanel.host/authorize", "POST");

            // Create the JSON object
            SteamSessionAuthorizationTicket ticket = new SteamSessionAuthorizationTicket() { ticket = BitConverter.ToString(_ticket).Replace("-", string.Empty) };
            string json = JsonUtility.ToJson(ticket);
            byte[] body = Encoding.UTF8.GetBytes(json);

            request.uploadHandler = new UploadHandlerRaw(body);

            // Set the content type to JSON
            request.SetRequestHeader("Content-Type", "application/json");

            UnityWebRequestAsyncOperation operation = request.SendWebRequest();

            while (!operation.isDone) yield return null;

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Successful login.");
            }
            else
            {
                Debug.LogError("Error authorizing player: " + request.error);
            }
        }

        public static IEnumerator GetSessionSteamID(Action<CSteamID> OnCallback, Action<string> OnError)
        {
            UnityWebRequest request = new UnityWebRequest("https://unity-netcode-project-njs.xrdxno.easypanel.host/client/session-id", "GET");

            request.downloadHandler = new DownloadHandlerBuffer();

            UnityWebRequestAsyncOperation operation = request.SendWebRequest();

            while (!operation.isDone) yield return null;
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Result: {request.downloadHandler.text}");
                OnCallback?.Invoke(new CSteamID());
                yield break;
            }
                
            OnError?.Invoke("Error getting session id: " + request.error);
        }

        public static IEnumerator SessionLogout()
        {
            UnityWebRequest request = new UnityWebRequest("https://unity-netcode-project-njs.xrdxno.easypanel.host/logout", "POST");

            request.downloadHandler = new DownloadHandlerBuffer();

            UnityWebRequestAsyncOperation operation = request.SendWebRequest();

            while (!operation.isDone) yield return null;

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"SUCCESS: {request.downloadHandler.text}");
            }
            else
            {
                Debug.LogError("Error logging out session: " + request.error);
            }
        }

        #region Obsolete

        // [Obsolete("Created as part of a tutorial series, kept for reference.")]
        // public static async Task<JSON.Player> GetPlayer(string steam_id)
        // {
        //     string response = await ServerRequest.GetRequest(string.Format(URL("/player/steam/{0}"), steam_id));
        //     Debug.Log($"{response}");
        //     JSON.Player player = JsonUtility.FromJson<JSON.Player>(response);
        //     return player;
        // }

        // [Obsolete("Created for research, kept for reference.")]
        // public static async Task CreatePlayer(string steam_id, string gamertag)
        // {
        //     JSON.Player newPlayer = new JSON.Player
        //     {
        //         steam_id = steam_id,
        //         gamertag = gamertag
        //     };
        // 
        //     string json = JsonUtility.ToJson(newPlayer);
        // 
        //     UnityWebRequest request = new UnityWebRequest(URL("/player/create"), "POST");
        //     byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        //     request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        //     request.downloadHandler = new DownloadHandlerBuffer();
        //     request.SetRequestHeader("Content-Type", "application/json");
        // 
        //     var operation = request.SendWebRequest();
        // 
        //     while (!operation.isDone)
        //         await Task.Yield();
        // 
        //     if (request.result == UnityWebRequest.Result.Success)
        //     {
        //         Debug.Log("Player created successfully");
        //         Debug.Log(request.downloadHandler.text);
        //     }
        //     else
        //     {
        //         Debug.LogError("Error creating player: " + request.error);
        //     }
        // }

        #endregion
    }

    namespace Requests
    {
        public class SteamSessionAuthorizationTicket
        {
            public string ticket;
        }
    }

}
