using UnityEngine;
using TMPro;

public class WaveCounterUI : MonoBehaviour
{
    [Tooltip("Reference to the TextMeshPro text component that shows the wave counter.")]
    public TMP_Text waveText;
    
    [Tooltip("Total number of waves (set this to the final wave for your game).")]
    

    void Update()
    {
        // Ensure EnemyManager.main is available.
        if (EnemyManager.main != null && waveText != null)
        {
            // Use the current wave number from the EnemyManager.
            int currentWave = EnemyManager.main.wave;
            // Update the text in the format "Wave: x / y"
            waveText.text = currentWave.ToString() + "/" + EnemyManager.main.totalWaves.ToString();
        }
    }
}
