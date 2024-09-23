using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public float currentHits = 0;
    public float maxHits;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Obstacle"))
        {
            currentHits++;
            if(currentHits == maxHits)
            {
                Destroy(gameObject);
            }
        }
    }
}
