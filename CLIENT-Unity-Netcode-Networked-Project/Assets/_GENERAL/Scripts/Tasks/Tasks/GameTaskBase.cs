using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Tasks
{
    [RequireComponent(typeof(GameTask))]
    public abstract class GameTaskBase : MonoBehaviour, IGameTaskInternal
    {
        [SerializeField] private bool _WaitForCompletion;
        public bool WaitForCompletion { get { return _WaitForCompletion; } }

        public IEnumerator Execute(System.Action<string> OnAbortTasksWithError)
        {
            yield return ExecuteInternal(OnAbortTasksWithError);
        }

        public abstract IEnumerator ExecuteInternal(Action<string> OnAbortTasksWithError);

        protected void AbortTasksWithError(System.Action<string> OnAbortTasksWithError, string text)
        {
            OnAbortTasksWithError.Invoke(text);
        }
    }

}
