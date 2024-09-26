using System.Collections.Generic;
using UnityEngine;

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
    public GameObject parentGameObject;

    // Dictionary to store cloned background layers
    private Dictionary<BackgroundLayer, GameObject> clonedLayers = new Dictionary<BackgroundLayer, GameObject>();

    void Start()
    {
        foreach (var layer in backgroundLayers)
        {
            if (layer.layer != null)
            {
                layer.objectWidth = layer.layer.GetComponent<SpriteRenderer>().bounds.size.x;
                layer.startingPos = layer.layer.transform.position;

                GameObject clone = Instantiate(layer.layer);

                clone.transform.SetParent(parentGameObject.transform);

                //float newPosition =+ layer.objectWidth;

                //clone.transform.localPosition = newPosition;

                clone.transform.Translate(new Vector3(layer.objectWidth, 0, 0), Space.Self);

                clonedLayers.Add(layer, clone);
            }
            else
            {
                Debug.LogWarning("Background layer is not assigned in the inspector.", this);
            }
        }
    }

    void Update()
    {
        foreach (var layer in backgroundLayers)
        {
            MoveBackground(layer, layer.layer);
            MoveBackground(layer, clonedLayers[layer]);


            if (layer.layer.transform.position.x < layer.startingPos.x - layer.objectWidth)
            {
                layer.layer.transform.Translate(new Vector3(layer.objectWidth * 2f, 0, 0), Space.Self);
            }

            if (clonedLayers[layer].transform.position.x < layer.startingPos.x - layer.objectWidth)
            {

                clonedLayers[layer].transform.Translate(new Vector3(layer.objectWidth * 2f, 0, 0), Space.Self);
            }
        }
    }

    void MoveBackground(BackgroundLayer layer, GameObject background)
    {
        background.transform.Translate(new Vector3(-layer.speed * Time.deltaTime, 0), Space.Self);
    }
}