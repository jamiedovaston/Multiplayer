using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Game.Model;
using System;
using Steamworks;

namespace GameServices
{
    public class PlayerServices
    {
        private static string BASE_URL = "https://unity-netcode-project-njs-netcode.xrdxno.easypanel.host{0}";
        private static string URL(string s) => string.Format(BASE_URL, s);

        public static async Task GetTime()
        {
            string response = await ServerRequest.GetRequest(URL("/tests/time"));
            Debug.Log(response);
        }

        public static async Task<CSteamID> AuthorizeSessionWithSteamAuthTicket(HAuthTicket _ticket)
        {
            UnityWebRequest request = new UnityWebRequest("https://unity-netcode-project-njs.xrdxno.easypanel.host/", "POST");

            byte[] body = Encoding.UTF8.GetBytes(_ticket.ToString());
            request.uploadHandler = new UploadHandlerRaw(body);
            request.downloadHandler = new DownloadHandlerBuffer();

            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if(request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error authorizing player: " + request.error);
            }

            // temp
            return new CSteamID();
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
}
