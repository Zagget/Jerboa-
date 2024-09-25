using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject menuOverlay;
    public Button play;
    public Button credits;
    public Button exit;

    public event System.Action OnPlaying;

    void Start()
    {
        menuOverlay.SetActive(true);
        ObstacleCollision obstacleCollision = FindObjectOfType<ObstacleCollision>();
        if (obstacleCollision != null)
        {
            obstacleCollision.OnGameOver += OnGameOver;
        }
        else
        {
            Debug.LogWarning("ObstacleCollision component not found.");
        }

        play.onClick.AddListener(() => ClickButton("play"));
        credits.onClick.AddListener(() => ClickButton("credits"));
        exit.onClick.AddListener(() => ClickButton("exit"));
    }

    private void Update()
    {
        
    }

    void ClickButton(string button)
    {
        switch (button)
        {
            case "play":
                menuOverlay.SetActive(false);
                if (OnPlaying != null)
                {
                    OnPlaying.Invoke();
                }
                break;

            case "credits":
                break;

            case "exit":
                EditorApplication.isPlaying = false;
                //Application.Quit();
                break;
        }
    }

    void OnGameOver()
    {
        menuOverlay.SetActive(true);
    }
}