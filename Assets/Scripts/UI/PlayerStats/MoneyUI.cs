using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    // Reference the TextMeshProUGUI component in the inspector.
    [SerializeField] private TextMeshProUGUI moneyText = null;

    void Update()
    {
        if (Player.main != null)
        {
            moneyText.text = "$" + Player.main.money.ToString();
        }
    }
}