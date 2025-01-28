using System.Threading.Tasks;
using UnityEngine;

namespace Game.Tasks
{
    [RequireComponent(typeof(GameTask))]
    public abstract class GameTaskInternal : MonoBehaviour, IGameTaskInternal
    {
        public abstract Task Execute();
    }

}
