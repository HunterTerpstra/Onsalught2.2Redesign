using UnityEngine;
using UnityEngine.UI;

public class StartGameOverlay : MonoBehaviour
{
    // Reference to the overlay panel (this GameObject, for example)
    [SerializeField] private GameObject overlayPanel;
    
    // Call this method when the Play button is clicked.
    public void StartGame()
    {
        // Hide the overlay.
        if(overlayPanel != null)
            overlayPanel.SetActive(false);
        
        // Unpause the game.
        Time.timeScale = 1f;
        
        // Optionally enable other UI elements if needed.
    }
}
