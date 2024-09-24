using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject Obstacle1;
    public GameObject Obstacle2;
    public GameObject Obstacle3;

    GameObject[] obstacleArray;

    public float speed;

    float xVelocity;

    Rigidbody2D rb2D;

    bool hasSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        obstacleArray = new GameObject[] {Obstacle1, Obstacle2, Obstacle3};

        Invoke(nameof(SpawnObstacle), 5);

    }

    // Update is called once per frame
    void Update()
    {
        if(hasSpawned)
        {
            xVelocity += speed * Time.deltaTime;
            rb2D.velocity = new Vector2(xVelocity, rb2D.velocity.y);
        }   
    }

    public void SpawnObstacle()
    {
        GameObject currentObstacle = Instantiate(obstacleArray[Random.Range(0, obstacleArray.Length)], transform.position, transform.rotation);
        rb2D = currentObstacle.GetComponent<Rigidbody2D>();
        hasSpawned = true;

    }
}
