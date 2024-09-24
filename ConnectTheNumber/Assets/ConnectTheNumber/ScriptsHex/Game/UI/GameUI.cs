using UnityEngine;
using UnityEngine.UI;


namespace Ilumisoft.Hex
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField]
        Text scoreText;

        [SerializeField]
        Text highScoreText; 

        ScoreTween scoreTween;
        //ScoreTween highScoreTween; 


        int lastScore = 0;
        int lastHighScore = 0; 

        private void Awake()
        {
            scoreTween = new ScoreTween(scoreText);
            //highScoreTween = new ScoreTween(highScoreText); 
            UpdateHighScoreUI(); 
        }

        private void OnEnable()
        {
            Score.OnScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            Score.OnScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int currentScore)
        {
            scoreTween.Fade(lastScore, currentScore, 1.0f);
            lastScore = currentScore;

            /*if (currentScore > Highscore.Value)
            {
                int previousHighScore = Highscore.Value;
                Highscore.Value = currentScore; // Update high score

                // Animate high score text
                highScoreTween.Fade(previousHighScore, currentScore, 1.0f);
                lastHighScore = currentScore;
            }*/
        }

        private void UpdateHighScoreUI()
        {
            highScoreText.text = Highscore.Value.ToString();
        }
    }
}