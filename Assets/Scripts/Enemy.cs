using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health;
    public int pointsPerKill;
    NavMeshAgent aiEnemy;
    public Transform enemyDestination;
    GameController gameController;

    private void Start()
    {
        enemyDestination = GameObject.Find("EnemyTarget").transform;
    }
    private void Awake()
    {
        aiEnemy = GetComponent<NavMeshAgent>();
       // enemyDestination = GameObject.Find("EnemyTarget").gameObject;
        gameController = GameObject.Find("Player").GetComponent<GameController>();
    }

    private void Update()
    {
        aiEnemy.SetDestination(enemyDestination.position);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        gameController.enemiesKilled++;
        gameController.points += pointsPerKill;
        gameController.enemiesInScene--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(100);
            
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("End"))
        {
            gameController.health -= 10;
            gameController.enemiesInScene--;
            Destroy(gameObject);
        }
    }
}
