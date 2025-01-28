using System.Threading.Tasks;
using UnityEngine;
using Game.Model.DataAsset.Scene;

namespace Game.Managers
{
    public static class SceneManager
    {
        public static async Task LoadScene(string _id) => await LoadScene(Scene.Get(_id).buildIndex);
        public static async Task LoadScene(int _index)
        {
            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_index, UnityEngine.SceneManagement.LoadSceneMode.Single);

            while (!operation.isDone)
            {
                await Task.Yield();
            }
        }

        public static async Task LoadSceneAdditive(string _id) => await LoadSceneAdditive(Scene.Get(_id).buildIndex);
        public static async Task LoadSceneAdditive(int _index)
        {
            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_index, UnityEngine.SceneManagement.LoadSceneMode.Additive);

            while (!operation.isDone)
            {
                await Task.Yield();
            }
        }
    }
}