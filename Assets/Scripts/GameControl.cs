using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameControl : MonoBehaviour
{
    public Button playPauseButton;   // Assign in Inspector
    public Image buttonImage;        // The image on the button
    public Sprite playSprite;        // Sprite for "play"
    public Sprite pauseSprite;       // Sprite for "pause"
    public TMP_Text buttonText;      // Optional text label on the button
    public CanvasGroup uiCanvasGroup;  // The CanvasGroup component on your UI Canvas

    public GameObject GreyScreenPaused;

    private bool gameStarted = false;
    public bool isPaused = false;

    public static GameControl instance;

    void Awake(){
        if(instance != null && instance != this){
        Destroy(gameObject);
    } else {
        instance = this;
    }
    }

    public void OnPlayPauseButtonClicked()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            Time.timeScale = 1f;
            if (EnemyManager.main != null)
                EnemyManager.main.StartGame();

            // Switch to pause state.
            if (buttonImage != null)
                buttonImage.sprite = pauseSprite;
            if (buttonText != null)
                buttonText.text = "Pause";
            if (uiCanvasGroup != null)
            {
                uiCanvasGroup.interactable = true;
                uiCanvasGroup.blocksRaycasts = true;
            }
        }
        else
        {
            if (!isPaused)
            {
                // Pause the game.
                GreyScreenPaused.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;
                if (buttonImage != null)
                    buttonImage.sprite = playSprite;
                if (buttonText != null)
                    buttonText.text = "Unpause";
                if (uiCanvasGroup != null)
                {
                    uiCanvasGroup.interactable = false;
                    uiCanvasGroup.blocksRaycasts = false;
                }
            }
            else
            {
                // Unpause the game.
                GreyScreenPaused.SetActive(false);
                Time.timeScale = 1f;
                isPaused = false;
                if (buttonImage != null)
                    buttonImage.sprite = pauseSprite;
                if (buttonText != null)
                    buttonText.text = "Pause";
                if (uiCanvasGroup != null)
                {
                    uiCanvasGroup.interactable = true;
                    uiCanvasGroup.blocksRaycasts = true;
                }
            }
        }
    }
}
