using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuEntry : MonoBehaviour
{
    public GameObject entity;
    UI UI;
    Game Game;
    Button button;
    public int towerId;

    void Start()
    {
        UI = UI.Get();
        //Game = Game.Get();
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
    }

    private void Click() {
        //Game.TowerPlacementManager.Place(towerId);
        // UI.Castbar.Set(entity, CastbarUI.Type.Build);
        // UI.Castbar.Show();
    }
}
