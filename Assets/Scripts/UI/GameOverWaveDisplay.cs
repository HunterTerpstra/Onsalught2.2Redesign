using UnityEngine;
using TMPro;

public class GameOverWaveDisplay : MonoBehaviour
{
    // Assign this in the Inspector with the TextMeshPro component on your Game Over screen.
    [SerializeField] private TMP_Text waveText;

    void OnEnable()
    {
        // Ensure EnemyManager.main is available.
        if (EnemyManager.main != null && waveText != null)
        {
            // Retrieve the final wave and total waves from EnemyManager.
            int finalWave = EnemyManager.main.wave;
            int totalWaves = EnemyManager.main.totalWaves;
            
            // Display the information. For example:
            waveText.text = "Wave: " + finalWave.ToString() + " / " + totalWaves.ToString();
        }
        else
        {
            Debug.LogWarning("EnemyManager.main or waveText not assigned!");
        }
    }
}
