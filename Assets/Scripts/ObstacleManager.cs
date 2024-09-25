using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject Obstacle1;
    public GameObject Obstacle2;
    public GameObject Obstacle3;

    GameObject[] obstacleArray;
    GameObject currentObstacle;

    public float maxSpeed = 10f;
    public float speed;

    float xVelocity;
    float screenHalfWidthInWorldUnits;
    float halfObstacleWidth;

    Rigidbody2D rb2D;

    RampSpawner rampSpawner;

    bool hasSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        rampSpawner = FindAnyObjectByType<RampSpawner>();

        halfObstacleWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;

        obstacleArray = new GameObject[] { Obstacle1, Obstacle2, Obstacle3 };

        Invoke(nameof(SpawnObstacle), 3);

    }

    // Update is called once per frame
    void Update()
    {
        MoveObstacle();
        DestroyObstacle();
    }

    public void SpawnObstacle()
    {
        currentObstacle = new GameObject();

        Vector2 rampPos = rampSpawner.GetStartingPos();

        Vector2 spawnPos = rampPos + new Vector2(10, 1);

        currentObstacle = Instantiate(obstacleArray[Random.Range(0, obstacleArray.Length)], spawnPos, transform.rotation);
        rb2D = currentObstacle.GetComponent<Rigidbody2D>();
        hasSpawned = true;

    }

    public void MoveObstacle()
    {
        if (hasSpawned)
        {
            xVelocity += -speed * Time.deltaTime;
            xVelocity = Mathf.Clamp(xVelocity, -maxSpeed, 0);
            rb2D.velocity = new Vector2(xVelocity, rb2D.velocity.y);

        }



    }
    public Vector2 GetCurrentObstaclePos()
    {
        if(currentObstacle != null)
        {
            return currentObstacle.transform.position;

        }
        return Vector2.zero;
    }
    public bool DestroyObstacle()
    {
        if (currentObstacle != null && currentObstacle.transform.position.x < -screenHalfWidthInWorldUnits - halfObstacleWidth)
        {
            Destroy(currentObstacle.gameObject);
            SpawnObstacle();
            return true;
        }
        return false;
    }
}
