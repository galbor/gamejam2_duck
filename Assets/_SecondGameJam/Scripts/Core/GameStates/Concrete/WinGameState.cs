using _SecondGameJam.Scripts.Core.GameStates.Base;
using _SecondGameJam.Scripts.Core.Managers;
using UnityEngine;

namespace _SecondGameJam.Scripts.Core.GameStates.Concrete
{
    public class WinGameState : IGameState
    {
        private const string WIN_MUSIC_NAME = "DuckTheme";
        private GameManager _gameManager;

        public WinGameState(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void EnterState()
        {
            UIManager.Instance.ToggleWinGameMenu(true);
            AudioManager.Instance.PlayAudio(WIN_MUSIC_NAME, SoundType.Music);
        }

        public void ExitState()
        {
            UIManager.Instance.ToggleWinGameMenu(false);
        }

        public void UpdateState()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) ||
                Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.ChangeState(new MainMenuState(_gameManager));
            }
        }
    }
}