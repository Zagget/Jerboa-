using UnityEngine;
using System.Collections;
public class ObstacleCollision : MonoBehaviour
{
    public float currentHits = 0;
    public float maxHits;
    public float safeTime = 2f; 

    private bool isImmune = false;

    public event System.Action OnGameOver;

    public AudioSource hurt;

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (!isImmune && collider.gameObject.CompareTag("Obstacle"))
        {
            hurt.Play();
            currentHits++;
            GetComponent<SpriteRenderer>().color = Color.red;

            StartCoroutine(EnableImmunity());

            if (currentHits == maxHits)
            {
                OnGameOver?.Invoke();
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

    public IEnumerator EnableImmunity()
    {
        isImmune = true;  
        yield return new WaitForSeconds(safeTime); 
        isImmune = false;  
    }
}
