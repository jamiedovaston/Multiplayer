using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Tasks
{
    public class GameTask : MonoBehaviour 
    {
        [SerializeField] private string _taskName;
        public string TaskName { get { return _taskName; } }
        public IGameTaskInternal task { get { return GetComponent<IGameTaskInternal>(); } }
    }
}
