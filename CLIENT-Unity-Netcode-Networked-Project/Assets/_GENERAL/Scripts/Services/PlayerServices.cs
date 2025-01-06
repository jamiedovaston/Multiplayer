using System.Threading.Tasks;
using UnityEngine;
using Game.Model;
using Unity.VisualScripting;

namespace GameServices
{
    public class PlayerServices
    {
        private static string BASE_URL = "https://unity-netcode-project-njs-netcode.xrdxno.easypanel.host{0}";
        private static string URL(string s) => string.Format(BASE_URL, s);

        public static async Task<Player> GetPlayer(string _id)
        {
            string response = await ServerRequest.GetRequest(string.Format(URL("/player/{0}"), _id));
            Player player = JsonUtility.FromJson<Player>(response);
            return player;
        }

        public static async Task GetTime()
        {
            string response = await ServerRequest.GetRequest(URL("/tests/time"));
            Debug.Log(response);
        }
    }
}