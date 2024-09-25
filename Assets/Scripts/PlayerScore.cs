using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    ObstacleManager obstacleManager;
    Vector2 playerPos;

    bool hasPassedObstacle = false;

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
        
        if( obstaclePos.x < playerPos.x && hasPassedObstacle == false)
        {
            score = score + 10;
            hasPassedObstacle = true;
        }
        if(obstacleManager.DestroyObstacle() == true)
        {
            hasPassedObstacle = false;
        }
    }
}
