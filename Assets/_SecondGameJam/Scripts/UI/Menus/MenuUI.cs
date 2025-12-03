using _SecondGameJam.Scripts.Core.GameStates.Concrete;
using _SecondGameJam.Scripts.Core.Managers;
using UnityEngine;

namespace _SecondGameJam.Scripts.UI.Menus
{
    public class MenuUI : MonoBehaviour
    {
        private const string CLICK_SOUND = "Click";
        public void StartGame()
        {
            AudioManager.Instance.PlayAudio(CLICK_SOUND);
            GameManager.Instance.ChangeState(new PlayState(GameManager.Instance));
        }
        
        public void ResumeGame()
        {
            AudioManager.Instance.PlayAudio(CLICK_SOUND);
            GameManager.Instance.ChangeState(new PlayState(GameManager.Instance, false));
        }

        public void MainMenu()
        {
            AudioManager.Instance.PlayAudio(CLICK_SOUND);
            GameManager.Instance.ChangeState(new MainMenuState(GameManager.Instance));
        }
        
        public void RestartLevel()
        {
            AudioManager.Instance.PlayAudio(CLICK_SOUND);
            LevelManager.Instance.ReloadScene();
            GameManager.Instance.ChangeState(new PlayState(GameManager.Instance, false));
        }

        public void Options()
        {
            AudioManager.Instance.PlayAudio(CLICK_SOUND);
            // TODO: Show options screen - volume control.
        }
        
        public void QuitGame()
        {
            AudioManager.Instance.PlayAudio(CLICK_SOUND);
            GameManager.Instance.ChangeState(new ExitGameState());
        }
    }
}