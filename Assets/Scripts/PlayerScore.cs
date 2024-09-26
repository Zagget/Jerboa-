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
        

        obstacleManager = FindObjectOfType<ObstacleManager>();
        InvokeRepeating(nameof(Score), 0, 1);
    }

    public void Score()
    {
        if (!isPlaying) return;
        score++;
        scoreText.text = $"{score}";      
            
        return;
    }

    public void AddScore(int points)
    {
        if (!isPlaying) return;
        score += points;
        scoreText.text = $"{score}";
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = $"{score}";

    }

    public void SetPlaying(bool playingState)
    {
        isPlaying = playingState;

        if(!playingState)
        {
            if(score > highScore)
            {
                highScore = score;



                highScoreText.text = $"{highScore}";
            }
        }
    }

}
