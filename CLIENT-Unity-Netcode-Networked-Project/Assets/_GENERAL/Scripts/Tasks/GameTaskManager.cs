using Game.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Managers
{
    public class GameTaskManager : MonoBehaviour
    {
        [SerializeField] private string m_ID;
        public string id { get { return m_ID; } }

        private void OnEnable()
        {
            collection.Add(this);
        }

        private void OnDisable()
        {
            collection.Remove(this);
        }

        public async Task ExecuteTasks()
        {
            List<IGameTaskable> tasks = GetComponentsInChildren<IGameTaskable>().ToList();

            if (!tasks.Any())
            {
                Debug.LogError($"No tasks in task manager!", this);
                return;
            }

            for(int i = 0; i < tasks.Count; i++)
            {
                await tasks[i].ExecuteInternal();
            }
        }

        private static List<GameTaskManager> collection = new List<GameTaskManager>();
        public static GameTaskManager Get(string _id)
        {
            for(int i = 0; i < collection.Count; i++)
            {
                if(collection[i].id == _id)
                    return collection[i];
            }

            Debug.LogError($"No manager was found with ID: {_id}");
            return null;
        }
    }
}