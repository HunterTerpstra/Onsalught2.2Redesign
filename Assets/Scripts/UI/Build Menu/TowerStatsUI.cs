using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerStatsUI : MonoBehaviour {
    [Header("Header Panel")]
    public TMP_Text towerNameText;   // For the tower’s name
    public TMP_Text costText;        // (Optional) For the cost
    public RawImage iconImage;          // (Optional) For the icon

    [Header("Stats Descriptions")]
    public TMP_Text descriptionText;
    public TMP_Text damageText;
    public TMP_Text rangeText;
    public TMP_Text fireRateText;

    [Header("Cursor Follow Settings")]
    // Reference to the canvas that this UI panel is on.
    public Canvas parentCanvas;
    // Optional offset so the panel doesn’t sit directly under the mouse.
    public Vector2 offset;

    // Whether the panel should follow the cursor.
    public bool followCursor = true;

    /// <summary>
    /// Updates and shows the hover UI with stats from the given Tower.
    /// </summary>
    /// <param name="tower">The Tower component to extract stats from.</param>
    public void ShowStats(Tower tower) {
        towerNameText.text = tower.gameObject.name;
        // For example, if you had a cost field:
        costText.text = "Cost: " + tower.cost.ToString();
        descriptionText.text = tower.towerDescription;
        damageText.text = "Damage: " + tower.damage.ToString();
        rangeText.text = "Range: " + tower.range.ToString();
        fireRateText.text = "Fire Rate: " + tower.fireRate.ToString();
        // If you had an icon, assign it:
        if (tower.upgradeIcon != null && iconImage != null) {
            iconImage.texture = tower.upgradeIcon.texture;
        }

        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the stats hover UI.
    /// </summary>
    public void HideStats() {
        gameObject.SetActive(false);
    }

   void Update() {
    if (followCursor && gameObject.activeSelf) {
        // Directly set the position to the mouse position plus your desired offset.
        GetComponent<RectTransform>().position = Input.mousePosition + (Vector3)offset;
    }
}
}