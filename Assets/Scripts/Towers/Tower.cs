using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [Header("Tower Stats")]
    public float range = 8f;
    public int damage = 25;
    public float fireRate = 1f;
    public int cost = 100;
    public string towerDescription = "This is a tower";
  
    [Header("Targetting mode")]
    public bool first = true;
    public bool last = false;
    public bool strong = false;

    [Header("UI Icon")]
    // This icon is used in the upgrade panel (it can be different from the in‑game sprite)
    public Sprite upgradeIcon;

    

    [NonSerialized] public GameObject target;
    private float cooldown = 0f;
    
    [Header("Effects")]
    [SerializeField] private GameObject fireEffect;


    void Start()
    {
        
    }

    void Update()
    {
        cooldown += Time.deltaTime;
        if (target)
        {
            // Always update turret rotation to face the target.
            transform.up = target.transform.position - transform.position;
            
            // Increment the cooldown timer every frame.
            
            
            // If enough time has passed, fire at the target.
            if (cooldown >= 1f / fireRate)
            {
                target.GetComponent<Enemy>().TakeDamage(damage);
                cooldown = 0f;
                StartCoroutine(FireEffect());
            }
        }
    }

    IEnumerator FireEffect(){
        fireEffect.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        fireEffect.SetActive(false);
    }
}
