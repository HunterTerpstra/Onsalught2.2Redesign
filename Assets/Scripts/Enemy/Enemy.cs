using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    
    public int health = 50;
    public int moneyReward = 1;
    public int maxHealth; // Store the initial health as max

    [SerializeField] private float movespeed = 2f;
    private Rigidbody2D rb;
    private Transform checkpoint;
    [NonSerialized] public int index = 0;
    [NonSerialized] public float distance = 0;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        maxHealth = health; // Set maxHealth at start
    }

    void Start(){
        checkpoint = EnemyManager.main.checkpoints[index];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (checkpoint.position - transform.position).normalized;
        //transform.right = checkpoint.position - transform.position;
        rb.velocity = direction * movespeed;
    }

    void Update()
    {
        
        checkpoint = EnemyManager.main.checkpoints[index];
        distance = Vector2.Distance(transform.position, EnemyManager.main.checkpoints[index].position);

        if (Vector2.Distance(transform.position, checkpoint.position) < 0.1f)
        {
            Debug.Log("Reached checkpoint");
            index++;

            if (index >= EnemyManager.main.checkpoints.Length){
                Player.main.LoseLife();
                Destroy(gameObject);
            }
            
        }
    }

    public void TakeDamage(int damage){
        health -= damage;
        if (health <= 0){
            Destroy(gameObject);
            Player.main.money += moneyReward;

        }
    }
}
