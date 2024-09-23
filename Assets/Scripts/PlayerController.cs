using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 8f;
    public float acceleration = 5f;
    public float deceleration = 10f;

    Vector2 velocity = Vector2.zero;

    Rigidbody2D rb2D;

    void Start()
    {
        Application.targetFrameRate = 60;
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        velocity += new Vector2(x * acceleration * Time.deltaTime, y * acceleration * Time.deltaTime);

        ApplyDeceleration(x, y);

        rb2D.velocity = new Vector2(velocity.x, velocity.y);
    }

    void ApplyDeceleration(float x, float y)
    {
        if (x == 0 || (x < 0 == velocity.x > 0))
        {
            velocity.x *= 1 - (deceleration * Time.fixedDeltaTime);
        }

        if (y == 0 || (y < 0 == velocity.y > 0))
        {
            velocity.y *= 1 - (deceleration * Time.fixedDeltaTime);
        }
    }
}