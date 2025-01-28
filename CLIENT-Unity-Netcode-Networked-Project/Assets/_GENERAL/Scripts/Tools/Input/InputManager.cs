using UnityEngine;

namespace Game.Tools
{
    public static class InputManager
    {
        public static Input_Player input;

        [RuntimeInitializeOnLoadMethod]
        private static void Initialse()
        {
            input = new Input_Player();
        }
    }
}
