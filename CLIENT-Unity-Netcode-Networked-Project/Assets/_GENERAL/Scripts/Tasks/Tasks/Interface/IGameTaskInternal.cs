using System.Collections;

namespace Game.Tasks
{
    public interface IGameTaskInternal
    {
        public bool WaitForCompletion { get; }
        public IEnumerator Execute(System.Action<string> OnAbortTasksWithError);
    }

}