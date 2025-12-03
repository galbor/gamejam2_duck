using UnityEngine;
using UnityEngine.SceneManagement;

namespace _SecondGameJam.Scripts.Core
{
    public class Restarter : MonoBehaviour
    {
        [SerializeField] private KeyCode _restartKey = KeyCode.R;
        private bool _restart;

        private void Awake()
        {
            _restart = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(_restartKey) && !_restart)
            {
                _restart = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}