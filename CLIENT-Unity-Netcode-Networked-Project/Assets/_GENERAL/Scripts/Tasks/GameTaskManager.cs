using Game.Tasks;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Managers
{
    public class GameTaskManager : MonoBehaviour
    {
        [SerializeField] private List<GameTask> tasks = new List<GameTask>();

        public virtual void Execute(Action OnSuccess, Action<string> OnAbortedWithError)
        {
            StartCoroutine(ExecuteTasksInOrder(OnSuccess, OnAbortedWithError));
        }

        [ContextMenu("Execute Task List")]
        public virtual void Execute()
        {
            StartCoroutine(ExecuteTasksInOrder(() => { }, DefaultErrorHandler));
        }

        private IEnumerator ExecuteTasksInOrder(Action OnSuccess, Action<string> OnAbortedWithError)
        {
            foreach (GameTask task in tasks)
            {
                Debug.Log($"Task : {task.TaskName}");

                Coroutine coroutine = StartCoroutine(task.task.Execute(error =>
                {
                    OnAbortedWithError?.Invoke(error);
                    Debug.LogError($"Task {task.TaskName} aborted with OnAbortWithErrror: {error}");
                }));

                if (task.task.WaitForCompletion)
                {
                    yield return coroutine;
                }
            }
            OnSuccess?.Invoke();
        }

        private void DefaultErrorHandler(string _error)
        {
            // TODO : Error Logic required
            // TEMP
            Debug.LogError($"Error! : {_error}");
        }
    }
}