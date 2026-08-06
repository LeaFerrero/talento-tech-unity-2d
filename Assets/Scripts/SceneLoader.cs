using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
