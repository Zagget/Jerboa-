using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public float currentHits = 0;
    public float maxHits;

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Obstacle"))
        {
            currentHits++;
            GetComponent<SpriteRenderer>().color = Color.red;
            if(currentHits == maxHits)
            {
                Destroy(gameObject);
            }
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GetComponent<SpriteRenderer>().color = Color.white;            
        }
    }
}
