using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public BuildMenuUI BuildMenu;
    public StatsHoverUI StatsHover;

    public bool windowActive = false;

    public static UI Get() {
        return GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }
}
