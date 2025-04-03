using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathArrowManager : MonoBehaviour
{
    // The arrow prefab you created
    public GameObject arrowPrefab;
    
    // The spacing between arrows along the path
    public float arrowSpacing = 1.0f;
    
    // Optionally, a parent to hold all arrow instances
    public Transform arrowParent;
    
    // A list to store the spawned arrow objects
    private List<GameObject> arrows = new List<GameObject>();

    void Start()
    {
        // Optionally, get the checkpoints from EnemyManager
        if (EnemyManager.main != null)
        {
            CreateArrows(EnemyManager.main.checkpoints);
        }
    }

    public void CreateArrows(Transform[] checkpoints)
    {
        // Clear any existing arrows
        foreach(GameObject arrow in arrows)
        {
            Destroy(arrow);
        }
        arrows.Clear();

        // For each segment between checkpoints
        for (int i = 0; i < checkpoints.Length - 1; i++)
        {
            Vector3 start = checkpoints[i].position;
            Vector3 end = checkpoints[i + 1].position;
            Vector3 direction = (end - start).normalized;
            float segmentLength = Vector3.Distance(start, end);
            int arrowCount = Mathf.FloorToInt(segmentLength / arrowSpacing);
            for (int j = 0; j < arrowCount; j++)
            {
                // Position arrow at midpoint of each interval.
                Vector3 pos = start + direction * arrowSpacing * (j + 0.5f);
                GameObject arrow = Instantiate(arrowPrefab, pos, Quaternion.identity, arrowParent);
                
                // Rotate arrow to face the direction of the segment.
                // Here, assuming your arrow sprite points up (i.e., along its local up vector)
                arrow.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                
                arrows.Add(arrow);
            }
        }
    }
    
    // Optionally, if you want to show/hide arrows based on game state:
    public void SetArrowsActive(bool active)
    {
        foreach(GameObject arrow in arrows)
        {
            arrow.SetActive(active);
        }
    }
}
