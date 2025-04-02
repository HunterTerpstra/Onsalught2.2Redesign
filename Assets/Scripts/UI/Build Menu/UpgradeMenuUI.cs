using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UpgradeMenuUI : MonoBehaviour
{
    [Header("Upgrade Bars - Each stat gets 3 vertical bars")]
    public Image[] damageBars;
    public Image[] rangeBars;
    public Image[] fireRateBars;
    
    [Header("Stat Texts")]
    public TMP_Text damageText;
    public TMP_Text rangeText;
    public TMP_Text fireRateText;
    
    [Header("Cost & Upgrade Button")]
    public TMP_Text costText;      // Text under the upgrade button to show cost
    public Button upgradeButton;   // The upgrade button itself

    [Header("Sell Info")]
    public TMP_Text sellValueText; // Text below the sell button showing sell value

    [Header("Tower Icon")]
    public Image towerIconImage;

    [Header("Settings")]
    public int maxUpgradeLevel = 3;
    public Color filledColor = Color.green;
    public Color emptyColor = Color.gray;
    
    [Header("Tower Upgrade Reference")]
    public TowerUpgrades towerUpgrades;

    

    public bool isHovered = false;
    
    private void UpdateUpgradeBars(int level)
    {
        for (int i = 0; i < damageBars.Length; i++)
            damageBars[i].color = (i < level) ? filledColor : emptyColor;
        for (int i = 0; i < rangeBars.Length; i++)
            rangeBars[i].color = (i < level) ? filledColor : emptyColor;
        for (int i = 0; i < fireRateBars.Length; i++)
            fireRateBars[i].color = (i < level) ? filledColor : emptyColor;
    }

    void Update() {
    // Only update the interactable state; we don't want to override hover preview text.
    if (towerUpgrades != null && upgradeButton != null && Player.main != null)
    {
        int current = towerUpgrades.currentLevel;
        if (current < towerUpgrades.levels.Length)
        {
            int nextCost = towerUpgrades.levels[current].cost;
            bool canAfford = Player.main.money >= nextCost;
            if (upgradeButton.interactable != canAfford)
            {
                upgradeButton.interactable = canAfford;
                // Also update the cost text in case money changed
                if (costText != null)
                    costText.text = "Cost: $" + nextCost.ToString();
            }
        }
        else
        {
            // Maxed out.
            upgradeButton.interactable = false;
            if (costText != null)
                costText.text = "Maxed";
        }
    }
}
    
    /// <summary>
    /// Updates the UI to show the tower's current stats and the next upgrade cost.
    /// </summary>
    public void RefreshUI()
{
    if (towerUpgrades != null)
    {
        int current = towerUpgrades.currentLevel;
        UpdateUpgradeBars(current);
        
        Tower tower = towerUpgrades.GetComponent<Tower>();
        if (damageText != null)
            damageText.text = tower.damage.ToString();
        if (rangeText != null)
            rangeText.text = tower.range.ToString("F1");
        if (fireRateText != null)
            fireRateText.text = tower.fireRate.ToString("F1");
        
        // Update cost and button interactability:
        if (current < towerUpgrades.levels.Length)
        {
            int nextCost = towerUpgrades.levels[current].cost;
            if (costText != null)
                costText.text = "Cost: $" + nextCost.ToString();
            if (upgradeButton != null)
                upgradeButton.interactable = (Player.main != null && Player.main.money >= nextCost);
        }
        else
        {
            if (costText != null)
                costText.text = "Maxed";
            if (upgradeButton != null)
                upgradeButton.interactable = false;
        }

        // Update the sell value text
        UpdateSellValueText();

        // Update the tower icon:
        if (towerIconImage != null)
        {
            towerIconImage.sprite = tower.upgradeIcon;
        }
    }
}

    public void UpdateSellValueText()
    {
        if (towerUpgrades != null && sellValueText != null)
        {
            Tower tower = towerUpgrades.GetComponent<Tower>();
            int sellValue = tower.cost / 2;
            for (int i = 0; i < towerUpgrades.currentLevel; i++)
            {
                sellValue += towerUpgrades.levels[i].cost / 2;
            }
            sellValueText.text = "Sell: $" + sellValue.ToString();
        }
    }
    
 
    
    /// <summary>
    /// Updates the UI to show the preview of the next upgrade.
    /// </summary>
    public void UpdateDeltaPreview()
    {
        if (towerUpgrades != null)
        {
            int current = towerUpgrades.currentLevel;
            if (current < maxUpgradeLevel)
            {
                // Preview: fill bars as if upgraded.
                UpdateUpgradeBars(current + 1);
                
                Tower tower = towerUpgrades.GetComponent<Tower>();
                TowerUpgrades.Level next = towerUpgrades.GetNextUpgrade();
                if (next != null)
                {
                    int deltaDamage = next.damage - tower.damage;
                    float deltaRange = next.range - tower.range;
                    float deltaFireRate = next.fireRate - tower.fireRate;
                    
                    if (damageText != null)
                        damageText.text = tower.damage.ToString() + " (" + (deltaDamage >= 0 ? "+" : "") + deltaDamage.ToString() + ")";
                    if (rangeText != null)
                        rangeText.text = tower.range.ToString("F1") + " (" + (deltaRange >= 0 ? "+" : "") + deltaRange.ToString("F1") + ")";
                    if (fireRateText != null)
                        fireRateText.text = tower.fireRate.ToString("F1") + " (" + (deltaFireRate >= 0 ? "+" : "") + deltaFireRate.ToString("F1") + ")";
                }
            }
            else
            {
                RefreshUI();
            }
        }
    }
    
    private void Start()
    {
        RefreshUI();
    }
}
