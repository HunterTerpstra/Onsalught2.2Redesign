using UnityEngine;
using TMPro;

public class TowerButtonCost : MonoBehaviour
{
    // Reference to the Tower prefab (or a Tower instance with stats)
    public Tower towerPrefab;
    
    // Reference to the TextMeshProUGUI component that displays the cost.
    public TextMeshProUGUI costText;

    void Start()
    {
        if(towerPrefab != null && costText != null)
        {
            // Set the cost text to the value from the Tower class.
            costText.text = "$" + towerPrefab.cost.ToString();
        }
        else
        {
            Debug.LogWarning("Tower prefab or cost text is not assigned.");
        }
    }
}