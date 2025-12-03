using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _SecondGameJam.Scripts.Core.Managers
{
    /** Used for showing menus, HUD and in-game messages. */
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _gameOverMenu;
        [SerializeField] private GameObject _winGameMenu;


        /** Creates the singleton Instance */
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        public void ToggleMainMenu(bool show)
        {
            _mainMenu.SetActive(show);
        }

        public void TogglePauseMenu(bool show)
        {
            _pauseMenu.SetActive(show);
        }

        public void ToggleGameOver(bool show)
        {
            _gameOverMenu.SetActive(show);
        }

        public void ToggleWinGameMenu(bool show)
        {
            _winGameMenu.SetActive(show);
            StartCoroutine(SlowAppear(_winGameMenu, 1f));
        }

        IEnumerator SlowAppear(GameObject menu, float time)
        {
            Image image = menu.GetComponentInChildren<Image>();
            float original_opacity = image.color.a;
            float opacity = 0;
            while (opacity < original_opacity)
            {
                opacity = Math.Min(original_opacity, opacity + Time.fixedDeltaTime / time);
                image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
                yield return null;
            }
        }
    }
}