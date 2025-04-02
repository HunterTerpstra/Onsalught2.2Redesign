using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Reference to your UpgradeMenuUI component (assigned via the Inspector)
    public UpgradeMenuUI upgradeMenuUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (upgradeMenuUI != null)
        {
            upgradeMenuUI.isHovered = true;
            upgradeMenuUI.UpdateDeltaPreview();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (upgradeMenuUI != null)
        {
            upgradeMenuUI.isHovered = false;
            upgradeMenuUI.RefreshUI();
        }
    }
}
