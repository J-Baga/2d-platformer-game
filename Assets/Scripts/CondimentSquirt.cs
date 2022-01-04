using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondimentSquirt : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] GameObject collisionEffect;

    public Rigidbody2D rbSquirt;

    private void Start()
    {
        rbSquirt.velocity = transform.up * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Don't trigger any events when enemy projectile hits another enemy or player collectible.
        if (collision.name.Contains("Enemy") || collision.name.Contains("Avocado"))
        {
            return;
        }

        GameObject clone = Instantiate(collisionEffect, transform.position, transform.rotation);

        // Damage player if enemy projectile hits player.
        if (collision.gameObject.transform.parent != null && collision.gameObject.transform.parent.name == "Player")
        {
            Player player = collision.gameObject.transform.parent.GetComponent<Player>();
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
        Destroy(clone, 0.65f);
    }
}
