using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Game.Model;
using System;
using Steamworks;
using Game.Services.Requests;

namespace Game.Services
{
    public static class PlayerServices
    {
        private static Callback<GetTicketForWebApiResponse_t> m_GetSessionTicketForWebApi;

        private static string BASE_URL = "https://unity-netcode-project-njs-netcode.xrdxno.easypanel.host{0}";
        private static string URL(string s) => string.Format(BASE_URL, s);

        public static async Task GetTime()
        {
            string response = await ServerRequest.GetRequest(URL("/tests/time"));
            Debug.Log(response);
        }
        
        public static async Task AuthorizeSessionWithSteamAuthTicket(byte[] _ticket)
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

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Successful login.");

                // Assuming you need to parse the response to get CSteamID
                // This will depend on the actual response structure

                // var response = JsonUtility.FromJson<YourResponseObject>(request.downloadHandler.text);
                // return new CSteamID(response.steamid);
            }
            else
            {
                Debug.LogError("Error authorizing player: " + request.error);
            }
        }

        public static async Task<CSteamID> GetSessionSteamID()
        {
            UnityWebRequest request = new UnityWebRequest("https://unity-netcode-project-njs.xrdxno.easypanel.host/client/session-id", "GET");

            request.downloadHandler = new DownloadHandlerBuffer();

            UnityWebRequestAsyncOperation operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Result: {request.downloadHandler.text}");
                return new CSteamID();
            }
            else
            {
                Debug.LogError("Error getting session id: " + request.error);
            }

            return new CSteamID();
        }

        public static async Task SessionLogout()
        {
            UnityWebRequest request = new UnityWebRequest("https://unity-netcode-project-njs.xrdxno.easypanel.host/logout", "POST");

            request.downloadHandler = new DownloadHandlerBuffer();

            UnityWebRequestAsyncOperation operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

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

        [Obsolete("Created as part of a tutorial series, kept for reference.")]
        public static async Task<JSON.Player> GetPlayer(string steam_id)
        {
            string response = await ServerRequest.GetRequest(string.Format(URL("/player/steam/{0}"), steam_id));
            Debug.Log($"{response}");
            JSON.Player player = JsonUtility.FromJson<JSON.Player>(response);
            return player;
        }

        [Obsolete("Created for research, kept for reference.")]
        public static async Task CreatePlayer(string steam_id, string gamertag)
        {
            JSON.Player newPlayer = new JSON.Player
            {
                steam_id = steam_id,
                gamertag = gamertag
            };

            string json = JsonUtility.ToJson(newPlayer);

            UnityWebRequest request = new UnityWebRequest(URL("/player/create"), "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Player created successfully");
                Debug.Log(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error creating player: " + request.error);
            }
        }

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
