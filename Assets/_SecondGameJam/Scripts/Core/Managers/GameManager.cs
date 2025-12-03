using _SecondGameJam.Scripts.Core.GameStates.Base;
using _SecondGameJam.Scripts.Core.GameStates.Concrete;
using UnityEngine;

namespace _SecondGameJam.Scripts.Core.Managers
{
    /**
     * GamaManager is a Singleton that controls the state of the game.
     * Usage: GameManager.Instance.ChangeState(new *State(GameManager.Instance));
     */
    public class GameManager : MonoBehaviour
    {
        /** Singleton instance of the game manager. */
        public static GameManager Instance { get; private set; }

        public IGameState CurrentState { get; private set; }

        /** Creates a Singleton instance of the game manager. */
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            ChangeState(new MainMenuState(this));
        }

        /** Used to change the state of the game. */
        public void ChangeState(IGameState newState)
        {
            CurrentState?.ExitState();
            CurrentState = newState;
            CurrentState.EnterState();
        }

        private void Update()
        {
            CurrentState?.UpdateState();
        }
    }
}