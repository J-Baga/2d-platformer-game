using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] public Transform projectileSpawnEnemy;
    [SerializeField] int maxAmmo;

    private int currAmmo;

    private Enemy enemy;

    public GameObject enemyProjectilePrefab;

    [SerializeField] float shootCastTime;
    private float reloadCastTime;
    private bool canShoot;

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        canShoot = true;
        currAmmo = maxAmmo;
        reloadCastTime = shootCastTime * 1.5f;
    }

    private void FixedUpdate()
    {
        // Enemy can only shoot when aggro'd by player.
        if (enemy.aggro == true && canShoot == true && currAmmo > 0)
        {
            Shoot();
            currAmmo--;
        }
        // Enemy has an ammo system and needs to reload after shooting a number of times.
        if(currAmmo == 0)
        {
            StartCoroutine(Reload(reloadCastTime));
        }
    }

    private void Shoot()
    {
        canShoot = false;
        Instantiate(enemyProjectilePrefab, projectileSpawnEnemy.position, projectileSpawnEnemy.rotation);
        StartCoroutine(Wait(shootCastTime));
    }

    IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(time);
        currAmmo = maxAmmo;
    }

    IEnumerator Wait(float time)
    {
        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}
