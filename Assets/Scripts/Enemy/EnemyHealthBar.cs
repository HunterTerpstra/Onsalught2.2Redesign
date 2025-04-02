using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // The Image component that shows the fill.
    public Image fillImage;
    // Reference to the enemy this health bar belongs to.
    public Enemy enemy;

    // Colors for full and empty health.
    public Color fullColor = Color.green;
    public Color emptyColor = Color.red;

    void Start()
    {
        // If you haven't assigned the enemy, try to find it in the parent.
        if (enemy == null)
        {
            enemy = GetComponentInParent<Enemy>();
        }
    }

    void Update()
    {
        if (enemy != null)
        {
            // Calculate health percentage dynamically.
            float healthPercent = Mathf.Clamp01((float)enemy.health / enemy.maxHealth);
            // Update the fill amount.
            fillImage.fillAmount = healthPercent;
            // Lerp from empty (red) to full (green) based on health percentage.
            fillImage.color = Color.Lerp(emptyColor, fullColor, healthPercent);
        }
    }
}
