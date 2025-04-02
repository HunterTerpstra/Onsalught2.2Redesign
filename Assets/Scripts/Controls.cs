using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Controls : MonoBehaviour

{

    [SerializeField] private GameObject cannonTower;
    [SerializeField] private GameObject gattlingTower;
    [SerializeField] private GameObject rocketTower;
    [SerializeField] private GameObject laserTower;
    [SerializeField] private GameObject settingsMenuPanel;
    [SerializeField] private GameObject popoutMenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            TowerManager.instance.AttemptSelectTower(cannonTower);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)){
            TowerManager.instance.AttemptSelectTower(gattlingTower);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)){
            TowerManager.instance.AttemptSelectTower(rocketTower);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)){
            TowerManager.instance.AttemptSelectTower(laserTower);
        }
        else if (Input.GetMouseButtonDown(1)){
            TowerManager.instance.ClearSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenuPanel != null && popoutMenuPanel != null)
            {
                // Only toggle settings menu if no sub-panel is active.
                if (!IsAnySubPanelActive(popoutMenuPanel))
                {
                    bool isActive = settingsMenuPanel.activeSelf;
                    settingsMenuPanel.SetActive(!isActive);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space)){
            GameControl.instance.OnPlayPauseButtonClicked();
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            // Assuming TowerManager is a singleton.
            TowerManager.instance.ConfirmSell();
        }
    }

    private bool IsAnySubPanelActive(GameObject popoutMenuPanel)
{
    foreach (Transform child in popoutMenuPanel.transform)
    {
        if (child.gameObject.activeSelf)
        {
            return true;
        }
    }
    return false;
}
}
