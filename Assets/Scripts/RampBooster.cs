using UnityEngine;

public class RampBooster : MonoBehaviour
{
    public float maxBoostSpeed = 10f;
    public float acceleration = 5f;
    Vector2 boostDirection = Vector2.right;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerRb != null)
            {
                playerController.enabled = false;

                float currentSpeed = Vector2.Dot(playerRb.velocity, boostDirection.normalized);

                if (currentSpeed < maxBoostSpeed)
                {
                    Vector2 additionalVelocity = boostDirection.normalized * acceleration * Time.fixedDeltaTime;

                    playerRb.velocity += additionalVelocity;

                    if (Vector2.Dot(playerRb.velocity, boostDirection.normalized) > maxBoostSpeed)
                    {
                        playerRb.velocity = boostDirection.normalized * maxBoostSpeed;
                    }
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            playerController.enabled = true;
        }
    }
}