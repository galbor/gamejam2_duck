using _SecondGameJam.Scripts.Core.GameStates.Base;
using _SecondGameJam.Scripts.Core.Managers;
using UnityEngine;

namespace _SecondGameJam.Scripts.Core.GameStates.Concrete
{
    /** The state when a player presses the Start Game button */
    public class PlayState : IGameState
    {
        private const string GAME_MUSIC_NAME = "GameTheme";
        private const KeyCode RESTART_KEY = KeyCode.R;
        private readonly GameManager _gameManager;

        public PlayState(GameManager gameManager, bool isRestart = true)
        {
            _gameManager = gameManager;
            if (isRestart)
            {
                LevelManager.Instance.LoadScene("Game");
            }
        }

        public void EnterState()
        {
            AudioManager.Instance.PlayAudio(GAME_MUSIC_NAME, SoundType.Music, loop: true);
        }

        public void ExitState()
        {
            AudioManager.Instance.StopAudio(GAME_MUSIC_NAME, SoundType.Music);
        }

        public void UpdateState()
        {
            if (Input.GetKeyDown(RESTART_KEY))
            {
                GameManager.Instance.ChangeState(new PlayState(_gameManager, isRestart: true));
            }
            // else if (Input.GetKeyDown(KeyCode.P))
            // {
            //     GameManager.Instance.ChangeState(new PauseState(_gameManager));
            // }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.ChangeState(new MainMenuState(_gameManager));
            }
        }
    }
}