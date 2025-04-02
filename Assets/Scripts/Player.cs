using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player main;

    public int health = 10;
    public int money = 250;
    public HealthUI healthUI;
    
    [SerializeField] private string nextLevel = "Level2";

    // Reference to your GameOver UI panel (set this in the Inspector)
    public GameObject gameOverPanel;
    public CanvasGroup mainUICanvasGroup;

    void Awake()
    {
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Optionally, check for debugging input or other things.
    }

    public void LoseLife()
    {
        health--;
        if (healthUI != null)
        {
            healthUI.UpdateLives(health);
        }
        Debug.Log("Player Lives: " + health);
        if (health <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
{
    // Pause the game
    Time.timeScale = 0f;
    
    // Show the Game Over UI panel.
    if (gameOverPanel != null)
        gameOverPanel.SetActive(true);
    
    // Disable main UI interaction by setting the CanvasGroup properties
    if (mainUICanvasGroup != null)
    {
        mainUICanvasGroup.interactable = false;
        mainUICanvasGroup.blocksRaycasts = false;
    }
}

    // Called by the Home button on the Game Over UI
    public void GoToMainMenu()
{
    Time.timeScale = 1f;
    if (mainUICanvasGroup != null)
    {
        mainUICanvasGroup.interactable = true;
        mainUICanvasGroup.blocksRaycasts = true;
    }
    SceneManager.LoadScene("MainMenuScene");
}

    // Called by the Restart button on the Game Over UI
    public void RestartLevel()
    {
    Time.timeScale = 1f;
    if (mainUICanvasGroup != null)
    {
        mainUICanvasGroup.interactable = true;
        mainUICanvasGroup.blocksRaycasts = true;
    }
    SceneManager.LoadScene("Level1");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        if (mainUICanvasGroup != null)
        {
            mainUICanvasGroup.interactable = true;
            mainUICanvasGroup.blocksRaycasts = true;
        }
        SceneManager.LoadScene("Level1");
    }


}
