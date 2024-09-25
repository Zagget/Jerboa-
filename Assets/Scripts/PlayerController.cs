using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 8f;
    public float acceleration = 5f;
    public float deceleration = 10f;
    
    public float glideSpeed = 2f;
    public float glideHorizontalMultiplier = 0.95f;
    public float jumpForce = 5;
    
    bool groundCheck = false;
    bool isGliding = false;

    float xVelocity;

    float screenHalfWidthInWorldUnits;
    float halfPlayerWidth;

    Rigidbody2D rb2D;

    void Start()
    {
        Application.targetFrameRate = 60;
        rb2D = GetComponent<Rigidbody2D>();

        halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
    }

    void Update()
    {
        Movement();
        PlayerJump();
        PlayerGlide();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");

        xVelocity += x * acceleration * Time.deltaTime;
        xVelocity = Mathf.Clamp(xVelocity, -maxSpeed, maxSpeed);

        if (isGliding)
        {
            xVelocity *= glideHorizontalMultiplier;
        } 
        

        if (groundCheck)
        {
            rb2D.velocity = new Vector2(xVelocity, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(xVelocity, rb2D.velocity.y);
        }

        if (x == 0)
        {
            xVelocity -= Mathf.Sign(xVelocity) * deceleration * Time.deltaTime;
        }

        Vector2 playerPos = transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, -screenHalfWidthInWorldUnits + halfPlayerWidth, screenHalfWidthInWorldUnits - (5 * halfPlayerWidth));
        transform.position = playerPos;
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && groundCheck)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        }
        if (Input.GetButtonUp("Jump") && rb2D.velocity.y > 0)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.5f);
        }
    }

    void PlayerGlide()
    {
        if (Input.GetButton("Jump") && !groundCheck && rb2D.velocity.y < 0)
        {
            isGliding = true;
            rb2D.velocity = new Vector2(rb2D.velocity.x, -glideSpeed);
        }
        else
        {
            isGliding = false;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = true;
            isGliding = false;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = false;
        }
    }
}