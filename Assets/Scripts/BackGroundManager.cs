using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BackGroundManager : MonoBehaviour
{
    [System.Serializable]
    public class BackgroundLayer
<<<<<<< Updated upstream
    {
        public GameObject layer;  // The original background layer
        public float speed;       // The speed of the layer
        [HideInInspector] public float objectWidth; // Width of the layer
        [HideInInspector] public Vector3 startingPos; // Starting position of the layer
    }

    public List<BackgroundLayer> backgroundLayers;

    void Start()
    {

        float height = 2f * Camera.main.orthographicSize;
        float screenWidth = height * Camera.main.aspect;

        foreach (var layer in backgroundLayers)
        {
            if (layer.layer != null)
            {
                layer.objectWidth = layer.layer.GetComponent<SpriteRenderer>().bounds.size.x;
                layer.startingPos = layer.layer.transform.position;
            }
            else
            {
                Debug.LogWarning("Background layer is not assigned in the inspector.", this);
            }
        }
    }

    void Update()
    {
        for (int i = backgroundLayers.Count - 1; i >= 0; i--)
        {
            var layer = backgroundLayers[i];

            // Check if the layer has moved off-screen to the left
            if (layer.layer.transform.position.x <= -layer.objectWidth)
            {
                //layer.layer.transform.position = new Vector3(layer.objectWidth, layer.startingPos.y, layer.layer.transform.position.z);
                layer.layer.transform.Translate(new Vector3(layer.objectWidth, 0, 0), Space.Self);
            }
            MoveBackground(layer);
        }
    }

    void MoveBackground(BackgroundLayer layer)
    {
        // Move the layer to the left
        layer.layer.transform.Translate(Vector3.left * layer.speed * Time.deltaTime);
    }
=======
    {
        public GameObject layer;
        public float speed;
        [HideInInspector] public float objectWidth;
        [HideInInspector] public float rotation;  
        [HideInInspector] public Vector2 startingPos; 
        [HideInInspector] public Vector2 surrentPos;

    }

    public List<BackgroundLayer> backgroundLayers;
    List<BackgroundLayer> cloneLayers;


    GameObject backgroundLayer;
    GameObject newLayers;
        

    float width;
    float height;

    Vector2 startingPos;
    Vector2 currentPos;

    bool backgroundAlive;
    bool cloneBackgroundAlive;

    void Start()
    {
        height = 2f * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;

        for (int i = 0; i < backgroundLayers.Count; i++)
        {
            var layer = backgroundLayers[i];

            // Ensure the layer is assigned
            if (layer.layer != null)
            {
                layer.objectWidth = layer.layer.GetComponent<SpriteRenderer>().bounds.size.x;
                layer.startingPos = layer.layer.transform.position;
                layer.rotation = layer.layer.transform.eulerAngles.z;
                AddClone(layer);
            }
            else
            {
                Debug.LogWarning("Background layer is not assigned in the inspector.", this);
            }
        }
    }

    void AddClone(BackgroundLayer layer)
    {
        GameObject cloneLayer = Instantiate(layer.layer);

        cloneLayer.transform.position = new Vector3(
            layer.layer.transform.position.x + layer.objectWidth,
            layer.layer.transform.position.y,
            layer.layer.transform.position.z
        );

        backgroundLayers.Add(new BackgroundLayer
        {
            layer = cloneLayer,
            speed = layer.speed,
            objectWidth = layer.objectWidth,
            rotation = layer.rotation,
            startingPos = cloneLayer.transform.position
        });
    }

    void Update()
    {
        // Move each background layer
        foreach (var layer in backgroundLayers)
        {
            MoveBackground(layer);
        }
    }

    // Move the background layers leftward based on their speed
    private void MoveBackground(BackgroundLayer backgroundLayer)
    {
        backgroundLayer.layer.transform.Translate(Vector3.left * backgroundLayer.speed * Time.deltaTime);

        if (backgroundLayer.layer.transform.position.x < -width * 0.5f - backgroundLayer.objectWidth)
        {
            backgroundLayer.layer.transform.position = new Vector3(
                width * 0.5f + backgroundLayer.objectWidth,
                backgroundLayer.layer.transform.position.y,
                backgroundLayer.layer.transform.position.z
            );
        }
    }

    //public Vector2 GetStartingPos(GameObject backgroundLayer)
    //{



    //    //Vector2 middleLeft = new Vector2(-width * 0.5f, 0f);

    //    //float adjustedX = middleLeft.x - 2f;
    //    //float adjustedY = middleLeft.y + (height * 0.5f) - (objectWidth);

    //    //adjustedX += objectWidth * 0.5f * Mathf.Cos(rotation * Mathf.Deg2Rad);
    //    //adjustedY += objectWidth * 0.5f * Mathf.Sin(rotation * Mathf.Deg2Rad);

    //    //Vector2 startingPos = new Vector2(adjustedX, adjustedY);

    //    //return startingPos;
    //}
>>>>>>> Stashed changes
}
