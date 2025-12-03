using _SecondGameJam.Scripts.Core.GameStates.Base;
using _SecondGameJam.Scripts.Core.Managers;
using UnityEngine;

namespace _SecondGameJam.Scripts.Core.GameStates.Concrete
{
    /** The state when the player loses the game. */
    public class GameOverState : IGameState
    {
        private const string GAME_OVER_MUSIC_NAME = "PirateTheme";
        private const KeyCode RESTART_KEY = KeyCode.R;
        private readonly GameManager _gameManager;

        public GameOverState(GameManager gameManager)
        {
            _gameManager = gameManager;
        }


        public void EnterState()
        {
            Time.timeScale = 0;
            UIManager.Instance.ToggleGameOver(true);
            AudioManager.Instance.PlayAudio(GAME_OVER_MUSIC_NAME, SoundType.Music);
        }

        public void ExitState()
        {
            Time.timeScale = 1;
            UIManager.Instance.ToggleGameOver(false);
            AudioManager.Instance.StopAudio(GAME_OVER_MUSIC_NAME, SoundType.Music);
        }

        public void UpdateState()
        {
            if (Input.GetKeyDown(RESTART_KEY) || Input.GetKeyDown(KeyCode.Space) || 
                Input.GetKeyDown(KeyCode.Return))
            {
                _gameManager.ChangeState(new PlayState(_gameManager));
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                _gameManager.ChangeState(new MainMenuState(_gameManager));
            }
        }
    }
}