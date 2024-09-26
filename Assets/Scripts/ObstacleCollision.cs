using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class ObstacleCollision : MonoBehaviour
{
    public Image hp1;
    public Image hp2;
    public Image hp3;

    public Image deadHp1;
    public Image deadHp2;
    public Image deadHp3;

    public Animator hp1Animator;
    public Animator hp2Animator;
    public Animator hp3Animator;

    public RuntimeAnimatorController heartAnimationController;

    public float currentHits = 0;
    public float maxHits;
    public float safeTime = 2f;

    public AudioSource death;

    private bool isImmune = false;

    public event System.Action OnGameOver;

    public AudioSource hurt;

    public void ResetHP()
    {
        hp1.gameObject.SetActive(true);
        hp2.gameObject.SetActive(true);
        hp3.gameObject.SetActive(true);

        deadHp3.gameObject.SetActive(false);
        deadHp2.gameObject.SetActive(false);
        deadHp1.gameObject.SetActive(false);
    }
    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (!isImmune && collider.gameObject.CompareTag("Obstacle"))
        {
            hurt.Play();
            currentHits++;

                if (currentHits == 1)
                {
                    hp3Animator.runtimeAnimatorController = heartAnimationController;
                    deadHp3.gameObject.SetActive(true);
                }
                else if(currentHits == 2)
                {
                    hp2Animator.runtimeAnimatorController = heartAnimationController;
                    deadHp2.gameObject.SetActive(true);

                }
                else if (currentHits == 3)
                {
                    hp1Animator.runtimeAnimatorController = heartAnimationController;
                    deadHp1.gameObject.SetActive(true);

                }

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
