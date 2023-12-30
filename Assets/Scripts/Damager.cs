using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] int damage = 1;

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        GameObject otherGameObject = collision.gameObject;
        EnemyHealthSystem enemyObject = otherGameObject.GetComponentInChildren<EnemyHealthSystem>();

        if (enemyObject != null)
        {
            //Debug.Log(other.name);û
            enemyObject.TakeDamage(damage);
            Destroy(gameObject);
        }


    }

}
