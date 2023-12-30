using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DirectionalFire : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireDelay = 5f;

    [SerializeField] Transform firePoint;
    [SerializeField] float fireForce;

    private bool isFiring = false;

    public void Fire(Vector2 fireDirection)
    {
        if (isFiring)
        {
            return;
        }

        StartCoroutine(FireProjectile());

        IEnumerator FireProjectile()
        {
            isFiring = true;

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(fireDirection * fireForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(fireDelay);
            isFiring = false;

        }

    }

}
