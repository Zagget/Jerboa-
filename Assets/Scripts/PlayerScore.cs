using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    ObstacleManager obstacleManager;
    Vector2 playerPos;

   // float numbOfFlips = 0;

    int score = 0;
    public TMP_Text scoreText;

    void Start()
    {   
        obstacleManager = FindObjectOfType<ObstacleManager>();
        InvokeRepeating(nameof(Score), 0, 1);
    }

    public void Score()
    {                                                                               
        Vector2 obstaclePos = obstacleManager.GetCurrentObstaclePos();
        bool isDestroyed = obstacleManager.DestroyObstacle();
        playerPos = transform.position;

        scoreText.text = $"Score: {score}";
        score++;
            
        return;
    }
}
