using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avocado : MonoBehaviour
{
    [SerializeField] private GameObject collectEffectPrefab;
    private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Give player score if player touches it.
        if (collision.gameObject.name.Contains("Player") || collision.gameObject.name.Contains("toast"))
        {
            Destroy(gameObject);
            player.getCollectible();
            player.increaseScore(25);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            GameObject clone = Instantiate(collectEffectPrefab, transform.position, transform.rotation);
            Destroy(clone, 1.60f);
            return;
        }
        else
        {
            return;
        }
    }
}
