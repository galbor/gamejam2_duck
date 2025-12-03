using _SecondGameJam.Scripts.Core.GameStates.Base;
using _SecondGameJam.Scripts.Core.Managers;
using UnityEngine;

namespace _SecondGameJam.Scripts.Core.GameStates.Concrete
{
    /** The state when a player presses the Pause button */
    public class PauseState : IGameState
    {
        private readonly GameManager _gameManager;

        public PauseState(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void EnterState()
        {
            Time.timeScale = 0.0f;
            UIManager.Instance.TogglePauseMenu(true);
        }

        public void ExitState()
        {
            Time.timeScale = 1.0f;
            UIManager.Instance.TogglePauseMenu(false);
        }

        public void UpdateState()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                GameManager.Instance.ChangeState(new PlayState(_gameManager, false));
            }
            else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.ChangeState(new PlayState(_gameManager, isRestart: false));
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.ChangeState(new MainMenuState(_gameManager));
            }
        }
    }
}