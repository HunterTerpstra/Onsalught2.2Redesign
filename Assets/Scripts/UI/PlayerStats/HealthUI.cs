using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    
    public TMP_Text livesText;

    void Start()
    {
        livesText.text = Player.main.health.ToString();
    }

    public void UpdateLives(int lives)
    {
        livesText.text = lives.ToString();
    }

    

}
