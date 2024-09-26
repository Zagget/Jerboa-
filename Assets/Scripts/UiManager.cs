using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject menuOverlay;
    public GameObject creditsOverlay;
    public Button play;
    public Button credits;
    public Button exit;

    public event System.Action OnPlaying;

    void Start()
    {
        creditsOverlay.SetActive(false);
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            creditsSettings();
        }
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
                creditsOverlay.SetActive(true);
                menuOverlay.SetActive(false);

                break;

            case "exit":
                EditorApplication.isPlaying = false;
                //Application.Quit();
                break;
        }
    }

    void creditsSettings()
    {
        creditsOverlay.SetActive(false);
        menuOverlay.SetActive(true);
    }

    void OnGameOver()
    {
        menuOverlay.SetActive(true);
    }
}