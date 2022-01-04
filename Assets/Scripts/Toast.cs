using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] int damage = 1;
    [SerializeField] private GameObject collisionEffect;

    public Rigidbody2D rbToast;

    void Start()
    {
        // Shoot toast in the direction the toaster is facing.
        rbToast.velocity = transform.up * speed;
    }

    private void OnBecameInvisible()
    {
        // Destroy object when toast is out of camera range.
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Don't trigger anything if toast hits player.
        if (collision.name.Contains("Player"))
        {
            return;
        }
        // Destroy itself when it hits another prefab (enemy projectile).
        else if (collision.name.Contains("Prefab"))
        {
            Destroy(gameObject);
            return;
        }

        GameObject clone = Instantiate(collisionEffect, transform.position, transform.rotation);
        Enemy enemy = collision.GetComponent<Enemy>();
        
        // Make enemy take damage if it hits enemy.
        if (enemy != null)
        {
            clone.transform.SetParent(collision.gameObject.transform, true);
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
        Destroy(clone, 0.85f);
    }
}
