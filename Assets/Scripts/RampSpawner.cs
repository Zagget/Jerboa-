using UnityEngine;

public class RampSpawner : MonoBehaviour
{
    [Header("Ramp")]
    public Sprite spriteRamp;

    public float rotation = -20;
    public float rampSpeed = 3;
    public float rampMoveAmount = 1;

    GameObject ramp;
    Vector2 startingPos;
    Vector2 CurrentPos;
        
    float rampWidth, rampHeight;
    float width, height;
    float timeSinceLastUpdate, updateDelay;

    void Start()
    {
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;

        ramp = new GameObject("Ramp");
        SpriteRenderer spriteRenderer = ramp.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteRamp;

        ramp.transform.rotation = Quaternion.Euler(0, 0, rotation);

        rampWidth = spriteRenderer.sprite.bounds.size.x * ramp.transform.localScale.x;
        rampHeight = spriteRenderer.sprite.bounds.size.y * ramp.transform.localScale.y;

        BoxCollider2D collider = ramp.AddComponent<BoxCollider2D>();
        ramp.tag = "Ground";

        GetStartingPos();
        CurrentPos = startingPos;
        ramp.transform.position = startingPos;
    }

    void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;
        updateDelay = 1f / rampSpeed;
        
        if (timeSinceLastUpdate >= updateDelay)
        {
            timeSinceLastUpdate = 0;
            MoveRamp();
        }

    }

    private void MoveRamp()
    {
        CurrentPos.x -= rampSpeed * Time.deltaTime;
        CurrentPos.y += rampSpeed * Time.deltaTime;

        ramp.transform.position = CurrentPos;
    }

    void GetStartingPos()
    {
        Vector2 middleLeft = new Vector2(-width * 0.5f, 0f);

        float adjustedX = middleLeft.x - 2f;
        float adjustedY = middleLeft.y + (height * 0.5f) - (rampHeight);

        adjustedX += rampWidth * 0.5f * Mathf.Cos(rotation * Mathf.Deg2Rad);
        adjustedY += rampWidth * 0.5f * Mathf.Sin(rotation * Mathf.Deg2Rad);

        startingPos = new Vector2(adjustedX, adjustedY);

        Debug.Log("Starting Position: " + startingPos);
    }
}
