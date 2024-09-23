using UnityEngine;

public class RampSpawner : MonoBehaviour
{
    [Header("Ramp")]
    public Sprite ramp;

    public float rotation = -20;
    public float rampSpeed = 3;
    float timeSinceLastUpdate;

    GameObject rampObject;
    float rampWidth;
    float rampHeight;

    Vector2 startingPos;
    float width;
    float height;

    void Start()
    {
        rampObject = new GameObject("Ramp");
        SpriteRenderer spriteRenderer = rampObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = ramp;

       

        rampWidth = spriteRenderer.sprite.bounds.size.x * rampObject.transform.localScale.x;
        rampHeight = spriteRenderer.sprite.bounds.size.y * rampObject.transform.localScale.y;

        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;

        rampObject.transform.rotation = Quaternion.Euler(0, 0, rotation);
        BoxCollider2D collider = rampObject.AddComponent<BoxCollider2D>();
        rampObject.tag = "Ground";

        GetStartingPos();
        GenerateHill();
    }

    void GenerateHill()
    {
        rampObject.transform.position = startingPos;

        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= rampSpeed)
        {
            Debug.Log("asdas");
        }

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
