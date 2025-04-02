using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TowerPlacement : MonoBehaviour
{
  
    [SerializeField] private SpriteRenderer rangeSprite;
    [SerializeField] private CircleCollider2D rangeCollider;
    [SerializeField] private Color gray;
    [SerializeField] private Color red;

    [NonSerialized] public bool isPlacing = true;
    private bool isRestricted = false;
    private int restrictedCount = 0;

    [SerializeField] private GameObject cancelPlacementText;

    void Awake()
    {
        rangeCollider.enabled = false;
    }

   
    void Update()
    {
        if (isPlacing)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
        if (isPlacing && cancelPlacementText != null)
        {
            cancelPlacementText.SetActive(true);
            Debug.Log("GOT HRE");
        }

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) && !isRestricted)
        {
            // Deduct cost from player's money
            int towerCost = GetComponent<Tower>().cost;
            if (Player.main != null)
            {
                Player.main.money -= towerCost;
                Debug.Log("Tower placed. Deducted cost: " + towerCost + ". New money: " + Player.main.money);
            }
            else
            {
                Debug.LogWarning("Player.main is null; cost not deducted.");
            }
            
            rangeCollider.enabled = true;
            isPlacing = false;
            rangeSprite.enabled = false;
            GetComponent<TowerPlacement>().enabled = false;
        }

        if (isRestricted)
        {
            rangeSprite.color = red;
        }
        else
        {
            rangeSprite.color = gray;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Restricted") || other.gameObject.CompareTag("Tower")) && isPlacing)
        {
            restrictedCount++;
            isRestricted = (restrictedCount > 0);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Restricted") || other.gameObject.CompareTag("Tower")) && isPlacing)
        {
            restrictedCount = Mathf.Max(0, restrictedCount - 1);
            isRestricted = (restrictedCount > 0);
        }
    }


}
