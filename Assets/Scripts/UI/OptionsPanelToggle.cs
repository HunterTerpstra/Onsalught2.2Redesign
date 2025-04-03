using UnityEngine;

public class OptionsPanelToggle : MonoBehaviour
{
    // Reference to the options panel in your scene.
    [SerializeField] private GameObject optionsPanel;

    // This function toggles the options panel on or off.
    public void ToggleOptionsPanel()
    {
        if (optionsPanel != null)
        {
            // Set the panel's active state to the opposite of its current state.
            optionsPanel.SetActive(!optionsPanel.activeSelf);
        }
        else
        {
            Debug.LogWarning("Options Panel is not assigned in OptionsPanelToggle.");
        }
    }
}
