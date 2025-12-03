using _SecondGameJam.Scripts.Events.Publishers;
using UnityEngine;

namespace _SecondGameJam.Scripts.Core.Managers
{
    /** Keeps track of the score.
     * Usage: ScoreManager.Instance.AddScore(10);
     */
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }
        public int Score { get; private set; }

        /** Initialize singleton Instance. */
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

        public void AddScore(int amount)
        {
            Score += amount;
            ScoreEventPublisher.ChangeScore(Score);
        }
    }
}