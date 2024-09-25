using UnityEditor;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public float speedAll = 5;

    public float speedStars = 2f;

    //[Header("Sprites")]
    //public Sprite Clouds;
    //public Sprite blue;
    //public Sprite stars;
    //public Sprite Mountains;
    //public Sprite dune1;
    //public Sprite dune2;
    //public Sprite purplemist;
    //public Sprite grassstraw;

    [Header("GameObjects")]
    public GameObject Clouds;
    public GameObject blue;
    public GameObject stars;
    public GameObject Mountains;
    public GameObject dune1;
    public GameObject dune2;
    public GameObject purplemist;
    public GameObject grassstraw;

    void Update()
    {
        // Move each background layer
        MoveBackground(Clouds, speedAll);
        MoveBackground(blue, speedAll);
        MoveBackground(stars, speedStars); // Stars move at a different speed
        MoveBackground(Mountains, speedAll);
        MoveBackground(dune1, speedAll);
        MoveBackground(dune2, speedAll);
        MoveBackground(purplemist, speedAll);
        MoveBackground(grassstraw, speedAll);
    }

    private void MoveBackground(GameObject backgroundLayer, float speed)
    {
        if (backgroundLayer != null)
        {
            // Move the background layer
            backgroundLayer.transform.Translate(Vector3.left * speed * Time.deltaTime);

            // Check if the background layer is out of view and reset its position
            if (backgroundLayer.transform.position.x < -15f) // Adjust this value as needed
            {
                // Reset to the right side of the screen (assuming a width of 15 units)
                backgroundLayer.transform.position = new Vector3(15f, backgroundLayer.transform.position.y, backgroundLayer.transform.position.z);
            }
        }
    }
}
