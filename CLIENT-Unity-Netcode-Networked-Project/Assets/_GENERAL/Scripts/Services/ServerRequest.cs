using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Services
{
    public class ServerRequest
    {
        public static async Task<string> GetRequest(string _url)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(_url))
            {
                await request.SendWebRequest();

                if(request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogError($"Could not connect to services.");
                    return null;
                }

                return request.downloadHandler.text;
            }
        }
    }
}