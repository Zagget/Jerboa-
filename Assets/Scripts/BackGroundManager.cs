using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [System.Serializable]
    public class BackgroundLayer
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
}
