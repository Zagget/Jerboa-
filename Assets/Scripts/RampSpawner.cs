using UnityEngine;

public class RampSpawner : MonoBehaviour
{
    [Header("Ramp")]
    public Sprite spriteRamp;

    public float rotation = -20;
    public float rampSpeed = 3;

    GameObject ramp;
    GameObject cloneRamp;
    Vector2 startingPos;
    Vector2 currentPos;
    Vector2 currentClonePos;

    float rampWidth, rampHeight;
    float width, height;

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
        cloneRamp = Instantiate(ramp);

        GetStartingPos();
        ramp.transform.position = startingPos;
        cloneRamp.transform.Translate(new Vector3(rampWidth, startingPos.y), Space.Self);
    }

    void Update()
    {
        MoveRamp();
        MoveClone();
    }

    void MoveRamp()
    {
        ramp.transform.Translate(new Vector3(-rampSpeed * Time.deltaTime, 0), Space.Self);
        currentPos = ramp.transform.position; 
    }

    void MoveClone()
    {
        cloneRamp.transform.Translate(new Vector3(-rampSpeed * Time.deltaTime, 0), Space.Self);
        currentClonePos = cloneRamp.transform.position;
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
