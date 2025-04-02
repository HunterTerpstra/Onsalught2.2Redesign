using UnityEngine;

public class TowerClickable : MonoBehaviour
{
    // Assign your custom cursor texture in the Inspector.
    public Texture2D clickableCursor; 
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void OnMouseEnter()
    {
        // Only change the cursor if this object is tagged "Tower"
        if (gameObject.CompareTag("Tower"))
        {
            Cursor.SetCursor(clickableCursor, hotSpot, cursorMode);
            Debug.Log("Cursor is on Tower" + gameObject.name);
        }
    }

    void OnMouseExit()
    {
        // Reset the cursor if this object is tagged "Tower"
        if (gameObject.CompareTag("Tower"))
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
