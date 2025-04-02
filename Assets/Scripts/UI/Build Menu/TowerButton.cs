using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    // Reference to the Tower prefab or a Tower data instance.
    public Tower towerPrefab;

    // Reference to the hover UI panel (its TowerStatsUI component).
    public TowerStatsUI statsUI;

    // Colors for affordable vs. unaffordable towers.
    public Color affordableColor = new Color(0.8f, 0.8f, 0.8f); // light gray
    public Color unaffordableColor = Color.red;

    // Cached references to the Button and Image components.
    private Button button;
    private Image backgroundImage;

    void Awake() {
        // Get the Button component (for interactability)
        button = GetComponent<Button>();
        // Get the Image component (assumed to be the background)
        backgroundImage = GetComponent<Image>();
    }

    void Update() {
        // Make sure we have a valid Player instance and towerPrefab.
        if (Player.main != null && towerPrefab != null) {
            bool canAfford = Player.main.money >= towerPrefab.cost;
            if (button != null)
                button.interactable = canAfford;
            if (backgroundImage != null)
                backgroundImage.color = canAfford ? affordableColor : unaffordableColor;
        }
    }

    // Called when the pointer enters the button area.
    public void OnPointerEnter(PointerEventData eventData) {
        if (statsUI != null && towerPrefab != null) {
            statsUI.ShowStats(towerPrefab);
            Debug.Log("Tower stats shown");
        }
    }

    // Called when the pointer exits the button area.
    public void OnPointerExit(PointerEventData eventData) {
        if (statsUI != null) {
            statsUI.HideStats();
        }
    }
}
