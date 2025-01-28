using UnityEngine;
using Game.Managers;

namespace Game.Application
{
    public class ApplicationManager : MonoBehaviour
    {
        private static GameTaskManager m_Initial;
        private static GameTaskManager m_Session;

        private async void Start()
        {
            m_Initial = GameTaskManager.Get("task_manager_initial");
            m_Session = GameTaskManager.Get("task_manager_session");

            await m_Initial.ExecuteTasks();
            await m_Session.ExecuteTasks();
        }
    }
}