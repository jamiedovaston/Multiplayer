using System.Threading.Tasks;
using UnityEngine;
using Game.Tools;
using Game.Managers;

namespace Game.Tasks
{
    public class GameTask_AwaitCredits : GameTaskInternal
    {
        public async override Task Execute()
        {
            Debug.Log("Executed!");
            await SceneManager.LoadSceneAdditive("scene_credits");
            await GameTaskManager.Get("task_credits").ExecuteTasks();
        }
    }

}
