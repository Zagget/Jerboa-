using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public GameObject menuOverlay;
    public Button play;
    public Button credits;
    public Button exit;


    void Start()
    {
        FindObjectOfType<ObstacleCollision>().OnGameOver += OnGameOver;
    }

    private void Update()
    {
        
    }

    void OnGameOver()
    {
        menuOverlay.SetActive(true);
    }
}