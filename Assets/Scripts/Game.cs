using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    //public ProgressionManager ProgressionManager;
    public TowerPlacementManager TowerPlacementManager;

    public static Game Get() {
        return GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }
}