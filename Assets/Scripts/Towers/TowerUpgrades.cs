using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerUpgrades : MonoBehaviour
{

    
    [System.Serializable]
    public class Level{
        public int damage = 25;
        public float range = 8f;
        public float fireRate = 1f;
        public int cost = 10;
    }

    [SerializeField] public Level[] levels = new Level[3];

    private Tower tower;
    [SerializeField] private TowerRange towerRange;
    [NonSerialized] public int currentLevel = 0;

    void Awake()
    {
        tower = GetComponent<Tower>();
    }

    public void Upgrade(){
        if (currentLevel < levels.Length){
            tower.damage = levels[currentLevel].damage;
            tower.range = levels[currentLevel].range;
            tower.fireRate = levels[currentLevel].fireRate;
            towerRange.UpdateRange();
            currentLevel++;
            Debug.Log("Upgraded to level " + currentLevel);
        }
    }
    
    // Helper method to return the next upgrade level (or null if maxed)
    public Level GetNextUpgrade()
    {
        if (currentLevel < levels.Length)
            return levels[currentLevel];
        return null;
    }
}

