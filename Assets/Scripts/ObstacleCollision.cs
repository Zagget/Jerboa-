using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public float currentHits = 0;
    public float maxHits;

    public event System.Action OnGameOver;

    public AudioSource hurt;

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Obstacle"))
        {
            hurt.Play();
            currentHits++;
            GetComponent<SpriteRenderer>().color = Color.red;
            if(currentHits == maxHits)
            {
                Destroy(gameObject);
                OnGameOver();
                currentHits = 0;
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
