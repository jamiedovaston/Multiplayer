using Game.Tasks;
using Game.Tools;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Tasks
{
    public class GameTask_Credit : GameTaskInternal
    {
        bool hasCancelled = false;
        public Animation m_CreditAnimation;
    
        public override async Task Execute()
        {
            InputManager.input.UI.Click.performed += Skip_Performed;

            m_CreditAnimation.Play();

            while(m_CreditAnimation.isPlaying && !hasCancelled)
            {
                await Task.Yield();
            }

            InputManager.input.UI.Click.performed -= Skip_Performed;
        }

        private void Skip_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            hasCancelled = true;
        }
    }
}
