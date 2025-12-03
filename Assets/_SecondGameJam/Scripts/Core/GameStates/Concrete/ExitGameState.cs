using _SecondGameJam.Scripts.Core.GameStates.Base;
using UnityEngine;

namespace _SecondGameJam.Scripts.Core.GameStates.Concrete
{
    public class ExitGameState : IGameState
    {
        public void EnterState()
        {
            Application.Quit();
        }

        public void ExitState()
        {
        }

        public void UpdateState()
        {
        }
    }
}