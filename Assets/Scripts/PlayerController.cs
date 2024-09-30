using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject player;

    SpriteRenderer spriteRenderer;
    public Sprite playerGlidingSprite;
    public Animator animator;
    public RuntimeAnimatorController GlidingAnimationController;
    public RuntimeAnimatorController BasicAnimationController;

    public float maxSpeed = 8f;
    public float acceleration = 5f;
    public float deceleration = 10f;

    public float rotationSpeed = 5f;
    public float glideSpeed = 2f;
    public float glideHorizontalMultiplier = 0.95f;
    public float jumpForce = 5;
    
    public bool groundCheck = false;
    bool isGliding = false;
    bool didFlip = false;
    bool isPlaying;

    Vector2 startingPos = Vector2.zero;

    float xVelocity;
    float screenHalfWidthInWorldUnits;
    float halfPlayerWidth;
    float lastRot = 0;

    public AudioSource jump;
    public AudioSource flip;

    PlayerScore playerScore;
    ObstacleCollision obstacleCollision1;

    Rigidbody2D rb2D;

    void Start()
    {

        Application.targetFrameRate = 60;
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        animator = player.GetComponent<Animator>();

        rb2D = GetComponent<Rigidbody2D>();

        obstacleCollision1 = FindObjectOfType<ObstacleCollision>();
        playerScore = FindObjectOfType<PlayerScore>();

        halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;

        
        startingPos = rb2D.transform.position;

        OnGameOver();

        UiManager sceneManager = FindObjectOfType<UiManager>();
        if (sceneManager != null)
        {
            sceneManager.OnPlaying += OnPlaying;
        }
        else
        {
            Debug.LogWarning("SceneManager not found.");
        }

        ObstacleCollision obstacleCollision = FindObjectOfType<ObstacleCollision>();
        if (obstacleCollision != null)
        {
            obstacleCollision.OnGameOver += OnGameOver;
        }
        else
        {
            Debug.LogWarning("ObstacleCollision component not found.");
        }


    }
    
    void OnPlaying()
    {
        transform.position = startingPos;
        rb2D.angularVelocity = 0f;
        rb2D.gravityScale = 1f;
        gameUI.SetActive(true);
        spriteRenderer.enabled = true;
        isPlaying = true;
        playerScore.ResetScore(); 
        playerScore.SetPlaying(true); 
        didFlip = false;
        obstacleCollision1.ResetHP(); 
    }

    void OnGameOver()
    {
        isPlaying = false; 
        rb2D.gravityScale = 0f; 
        rb2D.angularVelocity = 0f; 
        rb2D.velocity = Vector2.zero; 
        spriteRenderer.enabled = false;
        transform.position = startingPos; 
        playerScore.SetPlaying(false);
    }

    void Update()
    {
        if (isPlaying) 
        {
            PlayerJump();
            PlayerGlide();
            Rotate();
            CheckForFlip();
            Movement();
        }
      
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
        playerPos.x = Mathf.Clamp(playerPos.x, -screenHalfWidthInWorldUnits + halfPlayerWidth, screenHalfWidthInWorldUnits - (6 * halfPlayerWidth));
        transform.position = playerPos;
    }

    void Rotate()
    {
        if(Input.GetKey(KeyCode.Q) && groundCheck == false)
        {
            float deltaRotation = rotationSpeed * Time.deltaTime;
            rb2D.MoveRotation(rb2D.rotation + deltaRotation);
        }
        if (Input.GetKey(KeyCode.E) && groundCheck == false)
        {
            float deltaRotation = rotationSpeed * Time.deltaTime;
            rb2D.MoveRotation(rb2D.rotation - deltaRotation);
        }
    }

    void CheckForFlip()
    {
        float currRot = transform.eulerAngles.z;
        if(Mathf.Abs(currRot -  lastRot) > 180)
        {
                didFlip = true;
        }
        lastRot = currRot;
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && groundCheck)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            jump.Play();
           
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
            animator.runtimeAnimatorController = GlidingAnimationController;
        }
        else
        {
            isGliding = false;
            animator.runtimeAnimatorController = BasicAnimationController;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = true;
            isGliding = false;

            if (Mathf.Abs(transform.rotation.eulerAngles.z) < 25f && didFlip)  
            {
                playerScore.AddScore(10);  
                didFlip = false; 
            }

            else if(Mathf.Abs(transform.rotation.eulerAngles.z) > 180 && didFlip)
            {
               // OnGameOver();
            }
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