using _SecondGameJam.Scripts.Core.Managers;
using _SecondGameJam.Scripts.Events.Publishers;
using UnityEngine;

namespace _SecondGameJam.Scripts.UI.HUD
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _scoreText;
        private void OnEnable()
        {
            ScoreEventPublisher.OnScoreChanged += UpdateScoreDisplay;
            int score = ScoreManager.Instance.Score;
            UpdateScoreDisplay(score);
        }
        
        private void OnDisable()
        {
            ScoreEventPublisher.OnScoreChanged -= UpdateScoreDisplay;
        }

        private void UpdateScoreDisplay(int score)
        {
            _scoreText.text = $"Score: {score}";
        }
    }
}