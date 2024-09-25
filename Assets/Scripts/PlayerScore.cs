using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    ObstacleManager obstacleManager;

    int score = 0;
    public TMP_Text scoreText;

    void Start()
    {   
        obstacleManager = FindObjectOfType<ObstacleManager>();
        InvokeRepeating(nameof(Score), 0, 1);
    }

    public void Score()
    {
        scoreText.text = $"Score: {score}";
        score++;

        if(obstacleManager.GetCurrentObstaclePos > pla)
    }
}
