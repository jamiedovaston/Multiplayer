using System.Threading.Tasks;
using UnityEngine;

namespace Game.Tasks
{
    public class GameTask : MonoBehaviour, IGameTaskable
    {
        [SerializeField] private string m_ID;
        public string id { get { return m_ID; } }
        private IGameTaskInternal m_InternalTask { get { return GetComponent<IGameTaskInternal>(); } }
        public async Task ExecuteInternal()
        {
            Debug.Log($"Task Executed: {id}");
            await m_InternalTask.Execute();
        }
    }

}
