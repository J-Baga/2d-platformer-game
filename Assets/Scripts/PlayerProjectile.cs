using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnH, projectileSpawnV;
    [SerializeField] private GameObject toastPrefab, toasterHoriz, toasterVert;

    private Animator animHoriz, animVert;

    private float shootCastTime = 0.20f;
    private bool canShoot;

    private void Start()
    {
        animHoriz = toasterHoriz.GetComponent<Animator>();
        animVert = toasterVert.GetComponent<Animator>();
        projectileSpawnV.Rotate(0f, 0f, 270f);
        canShoot = true;
    }

    private void Update()
    {
        // Determine whether shooting cooldown is down.
        if (Input.GetKeyDown(KeyCode.A) && canShoot)
        {
            animHoriz.SetBool("isShooting", true);
            animVert.SetBool("isShooting", true);
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate toast prefab.
        if (toasterHoriz.gameObject.activeSelf)
        {
            Instantiate(toastPrefab, projectileSpawnH.position, projectileSpawnH.rotation);
            
        }
        else if (toasterVert.gameObject.activeSelf)
        {
            Instantiate(toastPrefab, projectileSpawnV.position, projectileSpawnV.rotation);
        }
        canShoot = false;

        // Initiate shooting cooldown time.
        StartCoroutine(Wait(shootCastTime));
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
        animHoriz.SetBool("isShooting", false);
        animVert.SetBool("isShooting", false);
    }
}
