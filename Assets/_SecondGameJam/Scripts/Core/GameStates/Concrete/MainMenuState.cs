using _SecondGameJam.Scripts.Core.GameStates.Base;
using _SecondGameJam.Scripts.Core.Managers;
using UnityEngine;

namespace _SecondGameJam.Scripts.Core.GameStates.Concrete
{
    /** The state when the player starts the game. */
    public class MainMenuState : IGameState
    {
        private const string MAIN_MENU_MUSIC_NAME = "GameTheme";
        private readonly GameManager _gameManager;

        public MainMenuState(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void EnterState()
        {
            LevelManager.Instance.LoadScene("MainMenu");
            AudioManager.Instance.PlayAudio(MAIN_MENU_MUSIC_NAME, SoundType.Music, loop: true);
        }

        public void ExitState()
        {
            AudioManager.Instance.StopAudio(MAIN_MENU_MUSIC_NAME, SoundType.Music);
        }

        public void UpdateState()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                GameManager.Instance.ChangeState(new PlayState(_gameManager));
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.ChangeState(new ExitGameState());
            }
        }
    }
}