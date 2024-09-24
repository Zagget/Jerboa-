using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 8f;
    public float acceleration = 5f;
    public float deceleration = 10f;

    public float jumpForce = 5;
    bool groundCheck = false;

    float xVelocity;

    Rigidbody2D rb2D;

    void Start()
    {
        Application.targetFrameRate = 60;
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        PlayerJump();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
 
        xVelocity += x * acceleration * Time.deltaTime;

        xVelocity = Mathf.Clamp(xVelocity, -maxSpeed, maxSpeed);

        if(groundCheck)
        {
            rb2D.velocity = new Vector2(xVelocity, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(xVelocity, rb2D.velocity.y);
        }

        if (x == 0 || (x < 0 == xVelocity > 0))
        {
            xVelocity *= 1 - (deceleration * Time.fixedDeltaTime);
        }
    }

    void PlayerJump()
    {
      if(Input.GetButtonDown("Jump") && groundCheck == true) 
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
        }
      if(Input.GetButtonUp("Jump") && rb2D.velocity.y > 0) 
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.5f);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = true;
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