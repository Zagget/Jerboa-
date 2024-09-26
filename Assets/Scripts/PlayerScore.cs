using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    ObstacleManager obstacleManager;

    int highScore = 0;
    int score = 0;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    bool isPlaying = false;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("Highscore", 0);
        highScoreText.text = $"Highscore: {highScore}";

        obstacleManager = FindObjectOfType<ObstacleManager>();
        InvokeRepeating(nameof(Score), 0, 1);
    }

    public void Score()
    {
        if (!isPlaying) return;
        score++;
        scoreText.text = $"Score: {score}";      
            
        return;
    }

    public void AddScore(int points)
    {
        if (!isPlaying) return;
        score += points;
        scoreText.text = $"Score: {score}";
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = $"Score: {score}"; 

    }

    public void SetPlaying(bool playingState)
    {
        isPlaying = playingState;

        if(!playingState)
        {
            if(score > highScore)
            {
                highScore = score;

                PlayerPrefs.SetInt("Highscore", highScore);
                PlayerPrefs.Save();

                highScoreText.text = $"Highscore: {highScore}";
            }
        }
    }

}
