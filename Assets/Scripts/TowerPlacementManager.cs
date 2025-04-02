using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TowerPlacementManager : MonoBehaviour
{
    [Header("Placement Settings")]
    public bool isPlacingTower = false;


    [Tooltip("All possible real tower prefabs")]
    public GameObject potentialPrefab0, potentialPrefab1, potentialPrefab2, potentialPrefab3;

    [Tooltip("All possible prefab outlines")]
    public GameObject potentialPreviewPrefab0, potentialPreviewPrefab1, potentialPreviewPrefab2, potentialPreviewPrefab3;

    [Tooltip("Selected Real tower prefab (e.g., cylinder)")]
    GameObject towerPrefab;

    [Tooltip("Selected Ghost/outline tower prefab (semi-transparent or outlined material)")]
    GameObject towerPreviewPrefab;


    // The currently visible tower preview
    private GameObject currentPreview;

    // Used to diffrentiate what tower we are placing
    private int towerId;

    UI UI;

    void Start()
    {
        UI = UI.Get();
    }

    void Update()
    {
        // Only do placement logic if we are currently in "placing" mode
        if (isPlacingTower)
        {

            if (towerId == 0)
            {
                towerPrefab = potentialPrefab0;
                towerPreviewPrefab = potentialPreviewPrefab0;
            }
            else if (towerId == 1)
            {
                towerPrefab = potentialPrefab1;
                towerPreviewPrefab = potentialPreviewPrefab1;
            }
            else if (towerId == 2)
            {
                towerPrefab = potentialPrefab2;
                towerPreviewPrefab = potentialPreviewPrefab2;
            }
            else if (towerId == 3)
            {
                towerPrefab = potentialPrefab3;
                towerPreviewPrefab = potentialPreviewPrefab3;
            }
            

            // If right clicked, cancel placement
            if (Input.GetMouseButtonUp(1))
            {
                Stop();
                return;
            }

            // Ray from camera to mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            
                // If we haven't created a preview object yet, instantiate it
                if (currentPreview == null)
                {
                    currentPreview = Instantiate(towerPreviewPrefab);
                }

           

                // Check if there's a valid grass tile at this cell
             
                // OPTIONAL: Change the preview color based on validity
                Renderer previewRenderer = currentPreview.GetComponentInChildren<Renderer>();
                if (previewRenderer != null)
                {
                    previewRenderer.material.color =  Color.green;
                }

                // If user left-clicks, attempt to place the real tower
                if (Input.GetMouseButtonDown(0))
                {
                        // Instantiate the actual tower
                        //Instantiate(towerPrefab, cellCenter, Quaternion.identity);


                        // If you only want to place one tower per "mode," exit placing mode
                        Destroy(currentPreview);
                        currentPreview = null;
                        isPlacingTower = false;
                        //UI.Castbar.Hide();
                
                }
            
            else
            {
                // If raycast hits nothing, either hide the preview or move it offscreen
                if (currentPreview != null)
                {
                    // Move it far away or disable it
                    currentPreview.transform.position = new Vector3(9999f, 9999f, 9999f);
                }
            }
        }
        else
        {
            // If we are NOT placing a tower, make sure no preview object remains
            if (currentPreview != null)
            {
                Destroy(currentPreview);
                currentPreview = null;
            }
        }
    }

    // Call this from a button to toggle placement mode on/off
    public void Place(int tower)
    {
        isPlacingTower = true;
        towerId = tower;
    }

    public void Stop() {
        if (!isPlacingTower) return;

        isPlacingTower = false;

        // If turning off, destroy any existing preview
        if (!isPlacingTower && currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }
    }
}