using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        
    }

    public void ShowGameOverMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(() =>
        {
            SceneLoader.Instance.LoadGameScene();  // Asegurate que exista este método en SceneLoader
        });

        // Configurar botón Menú Principal
        mainMenuButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.Instance.LoadMainMenu();  // Asegurate que exista este método en SceneLoader
        });
    }
}
