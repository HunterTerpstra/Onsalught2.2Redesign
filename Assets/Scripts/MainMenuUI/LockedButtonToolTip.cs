using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LockedButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea(2,5)]
    public string unlockInstructions = "Complete level 1 to unlock this feature!";

    // Assign the tooltip object (with a TextMeshProUGUI child) via Inspector
    public GameObject tooltipObject;  
    private TextMeshProUGUI tooltipText;

    // Optional offset so the tooltip doesn’t overlap the mouse pointer
    public Vector2 tooltipOffset = new Vector2(0f, 0f);

    // Track if the mouse is currently over this element
    private bool isHovering = false;

    void Awake()
    {
        if (tooltipObject != null)
        {
            tooltipText = tooltipObject.GetComponentInChildren<TextMeshProUGUI>();
            tooltipObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        if (tooltipObject != null && tooltipText != null)
        {
            tooltipObject.SetActive(true);
            tooltipText.text = unlockInstructions;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        if (tooltipObject != null)
        {
            tooltipObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isHovering && tooltipObject != null)
        {
            // Get a reference to the parent canvas
            Canvas parentCanvas = tooltipObject.GetComponentInParent<Canvas>();
            RectTransform canvasRect = parentCanvas.GetComponent<RectTransform>();

            // Use the camera associated with the canvas (for Screen Space - Camera)
            Camera cam = parentCanvas.worldCamera;

            // Convert the screen point to a local point in the canvas
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, cam, out localPoint);

            // Set the tooltip's local position relative to the canvas, plus your desired offset
            tooltipObject.GetComponent<RectTransform>().localPosition = localPoint + tooltipOffset;
        }
    }
}