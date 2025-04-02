using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject cannonTower;
    [SerializeField] private GameObject gattlingTower;
    [SerializeField] private GameObject rocketTower;
    [SerializeField] private GameObject laserTower;
    [SerializeField] private LayerMask towerLayer;

    [SerializeField] private GameObject buildMenuPanel;    // Build menu panel
    [SerializeField] private GameObject upgradeMenuPanel;  // Upgrade menu panel

    [SerializeField] private GameObject cancelPlacementText;

    // Reference to the sell confirmation popup panel (set in Inspector)
    [SerializeField] private GameObject sellConfirmationPanel;
    // Reference to the TMP_Text in the sell confirmation panel that displays the sell value
    [SerializeField] private TMP_Text sellConfirmationText;

    public TowerUpgrades selectedTowerUpgrades;
    
    private GameObject placingTower;
    private GameObject selectedTower;

    public static TowerManager instance;  // Singleton reference

    void Awake(){
    // If an instance already exists (unlikely if you're reloading the scene), destroy it:
    if(instance != null && instance != this){
        Destroy(gameObject);
    } else {
        instance = this;
    }
    // Reset state
    placingTower = null;
    selectedTower = null;
}

    void Update()
    {
     // Toggle cancel instruction text:

    

        if (placingTower != null && placingTower.GetComponent<TowerPlacement>().isPlacing)
        {
            if (cancelPlacementText != null)
                cancelPlacementText.SetActive(true);
        }
        else
        {
            if (cancelPlacementText != null)
                cancelPlacementText.SetActive(false);
        }

        if (placingTower){
            if (!placingTower.GetComponent<TowerPlacement>().isPlacing){
                placingTower = null;
            }
            
        }
       

        // Process left-click only when not over UI.
        if (placingTower == null && Input.GetMouseButtonDown(0) && 
            (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100f, towerLayer);
            
            if (hit.collider == null)
            {
                if (selectedTower != null)
                {
                    GameObject range = selectedTower.transform.GetChild(1).gameObject;
                    range.GetComponent<SpriteRenderer>().enabled = false;
                    selectedTower = null;
                }
                if (buildMenuPanel != null)
                    buildMenuPanel.SetActive(true);
                if (upgradeMenuPanel != null)
                    upgradeMenuPanel.SetActive(false);
            }
            else
            {
                GameObject clickedTower = hit.collider.gameObject;
                if (selectedTower == clickedTower)
                {
                    GameObject range = selectedTower.transform.GetChild(1).gameObject;
                    range.GetComponent<SpriteRenderer>().enabled = false;
                    selectedTower = null;
                    if (buildMenuPanel != null)
                        buildMenuPanel.SetActive(true);
                    if (upgradeMenuPanel != null)
                        upgradeMenuPanel.SetActive(false);
                }
                else
                {
                    if (selectedTower != null)
                    {
                        GameObject range = selectedTower.transform.GetChild(1).gameObject;
                        range.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    selectedTower = clickedTower;
                    selectedTowerUpgrades = selectedTower.GetComponent<TowerUpgrades>();

                    GameObject newRange = selectedTower.transform.GetChild(1).gameObject;
                    newRange.GetComponent<SpriteRenderer>().enabled = true;
                    ShowUpgradeUI(selectedTower);
                }
            }
        }

        // if (Input.GetKeyDown(KeyCode.U) && selectedTower)
        // {
        //     selectedTower.GetComponent<TowerUpgrades>().Upgrade();
        // }
    }

    public void ClearSelected(){
        if (placingTower){
            Destroy(placingTower);
            placingTower = null;
        }
    }

    private void SelectTower(GameObject tower){
        ClearSelected();
        placingTower = Instantiate(tower);
    }

    public void UpgradeSelectedTower()
    {
        if (selectedTowerUpgrades != null)
        {
            int current = selectedTowerUpgrades.currentLevel;
            if (current < selectedTowerUpgrades.levels.Length)
            {
                int nextCost = selectedTowerUpgrades.levels[current].cost;
                if (Player.main.money >= nextCost)
                {
                    Player.main.money -= nextCost;
                    Debug.Log("Deducted $" + nextCost + " for upgrade. New balance: $" + Player.main.money);
                    
                    selectedTowerUpgrades.Upgrade();

                    UpgradeMenuUI upgradeUI = upgradeMenuPanel.GetComponent<UpgradeMenuUI>();
                    if (upgradeUI != null)
                    {
                        upgradeUI.RefreshUI();
                        if (upgradeUI.isHovered)
                        {
                            upgradeUI.UpdateDeltaPreview();
                        }
                    }
                    
                    // Optionally update any money display here.
                }
                else
                {
                    Debug.Log("Not enough money to upgrade!");
                }
            }
            else
            {
                Debug.Log("Tower is already maxed out.");
            }
        }
        else
        {
            Debug.LogWarning("No tower selected for upgrade!");
        }
    }

    public void AttemptSelectTower(GameObject tower)
    {
        int cost = tower.GetComponent<Tower>().cost;
        if (Player.main != null && Player.main.money >= cost)
        {
            SelectTower(tower);
        }
        else
        {
            Debug.Log("Not enough money to purchase " + tower.name);
        }
    }

    public void OnTowerButtonPress(GameObject tower)
    {
        AttemptSelectTower(tower);
    }

    private void ShowUpgradeUI(GameObject tower)
    {
        if (buildMenuPanel != null)
            buildMenuPanel.SetActive(false);
        if (upgradeMenuPanel != null)
        {
            upgradeMenuPanel.SetActive(true);
            UpgradeMenuUI upgradeUI = upgradeMenuPanel.GetComponent<UpgradeMenuUI>();
            if (upgradeUI != null)
            {
                upgradeUI.towerUpgrades = tower.GetComponent<TowerUpgrades>();
                upgradeUI.RefreshUI();
            }
        }
    }

    // --- SELL FUNCTIONALITY ---
    // Call this method when the sell button is clicked.
    public void ShowSellConfirmation()
    {
        if (selectedTower != null)
        {
            int sellValue = CalculateSellValue(selectedTower);
            if (sellConfirmationText != null)
                sellConfirmationText.text = "Sell tower for $" + sellValue.ToString() + "?";
            if (sellConfirmationPanel != null)
                sellConfirmationPanel.SetActive(true);
        }
    }
    
    // Calculate the sell value:
    private int CalculateSellValue(GameObject tower)
    {
        Tower towerComp = tower.GetComponent<Tower>();
        int sellValue = towerComp.cost / 2;
        TowerUpgrades upgradesComp = tower.GetComponent<TowerUpgrades>();
        for (int i = 0; i < upgradesComp.currentLevel; i++)
        {
            sellValue += upgradesComp.levels[i].cost / 2;
        }
        return sellValue;
    }
    
    // Called by the Yes button on the sell confirmation popup.
    public void ConfirmSell()
    {
        if (selectedTower != null)
        {
            int sellValue = CalculateSellValue(selectedTower);
            if (Player.main != null)
            {
                Player.main.money += sellValue;
                Debug.Log("Sold tower for $" + sellValue + ". New balance: $" + Player.main.money);
            }
            Destroy(selectedTower);
            selectedTower = null;
            selectedTowerUpgrades = null;
            if (sellConfirmationPanel != null)
                sellConfirmationPanel.SetActive(false);
            if (buildMenuPanel != null)
                buildMenuPanel.SetActive(true);
            if (upgradeMenuPanel != null)
                upgradeMenuPanel.SetActive(false);
        }
    }
    
    // Called by the No button on the sell confirmation popup.
    public void CancelSell()
    {
        if (sellConfirmationPanel != null)
            sellConfirmationPanel.SetActive(false);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset your state here.
        placingTower = null;
        selectedTower = null;
        // Other reinitializations as needed.
    }
}
