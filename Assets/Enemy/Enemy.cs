

using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    
    //References
    private Animator anim;

    private EnemyPatrol enemyPatrol;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }


    [SerializeField] private int health = 3; // Kesehatan musuh

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Tambahkan animasi kematian atau efek di sini jika diperlukan
        Destroy(gameObject);
    }
}

    
  
    

