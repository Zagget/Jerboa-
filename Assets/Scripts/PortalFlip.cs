using UnityEngine;

public class PortalFlip : MonoBehaviour
{
    private Camera mainCamera;
    private bool isFlipped = false;
    public bool flipPlayerController = false;

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FlipScreen();

            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null && flipPlayerController)
            {
                //playerController.FlipPlayer();
            }
        }
    }

    void FlipScreen()
    {
        if (mainCamera != null)
        {
            isFlipped = !isFlipped;
            mainCamera.ResetWorldToCameraMatrix();
            mainCamera.ResetProjectionMatrix();

            mainCamera.projectionMatrix = mainCamera.projectionMatrix * Matrix4x4.Scale(new Vector3(isFlipped ? -1 : 1, 1, 1));
        }
    }

}