using System.Threading.Tasks;
using UnityEngine;
using Game.Model;

namespace GameServices
{
    public class PlayerServices
    {
        private static string BASE_URL = "https://unity-netcode-project-njs-netcode.xrdxno.easypanel.host{0}";
        private static string URL(string s) => string.Format(BASE_URL, s);

        public static async Task<JSON.Player> GetPlayer(string _id)
        {
            string response = await ServerRequest.GetRequest(string.Format(URL("/player/{0}"), _id));
            JSON.Player player = JsonUtility.FromJson<JSON.Player>(response);
            return player;
        }
        
        public static async Task GetTime()
        {
            string response = await ServerRequest.GetRequest(URL("/tests/time"));
            Debug.Log(response);
        }
    }
}