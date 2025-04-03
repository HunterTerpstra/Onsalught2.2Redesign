using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameControl : MonoBehaviour
{
    public Button playPauseButton;
    public Image buttonImage;
    public Sprite playSprite;
    public Sprite pauseSprite;
    public TMP_Text buttonText;
    public CanvasGroup uiCanvasGroup;
    public GameObject GreyScreenPaused;
    
    // Add a reference to the arrow container holding your pulsing arrows.
    public GameObject arrowContainer;
    public GameObject StartGameOverlay;

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
            // Unpause the game.
            Time.timeScale = 1f;
            
            // Hide the arrow container so the pulsing arrows disappear.
            if (arrowContainer != null)
            {
                arrowContainer.SetActive(false);
            }
            if (StartGameOverlay != null)
            {
                StartGameOverlay.SetActive(false);
            }

            if (EnemyManager.main != null)
                EnemyManager.main.StartGame();

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
