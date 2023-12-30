using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    //[SerializeField] int startHealth = 3;

    int health = 3;

    public void TakeDamage(int damage)
    {
        if (health <= 0) return;


        health -= damage;
        Debug.Log(health);


        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject);
        }

    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            health = startHealth;
        }
    }
    */
}
