using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BackGroundManager : MonoBehaviour
{
    [System.Serializable]
    public class BackgroundLayer
    {
        public GameObject layer; 
        public float speed; 
        [HideInInspector] public float objectWidth; 
        [HideInInspector] public Vector3 startingPos; 
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
        layer.layer.transform.Translate(Vector3.left * layer.speed * Time.deltaTime);
    }
}