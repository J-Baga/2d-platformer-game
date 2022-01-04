using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    [SerializeField] private GameObject toasterHoriz, toasterVert;

    public bool isHoriz;
    private bool canChangeForms;

    private float changeCastTime = 0.35f;

    private Player playerState;

    private void Start()
    {
        toasterHoriz.gameObject.SetActive(true);
        toasterVert.gameObject.SetActive(false);
        isHoriz = true;
        canChangeForms = true;
        playerState = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if changing form is not on cooldown and if player isn't dead.
        if (Input.GetKeyDown(KeyCode.S) && canChangeForms && playerState.isDead == false)
        {
            ChangeForm();
        }
    }

    /// <summary>
    /// Changes the toaster form from horizontal to vertical or vertical to horizontal,
    /// depending on which form is active at the time of key cast. Upon change, a cooldown
    /// is simulated to prevent player from potentially abusing the mechanic.
    /// </summary>
    private void ChangeForm()
    {
        // Change between horizontal and vertical forms, depending on the original form.
        if (isHoriz)
        {
            toasterVert.transform.position = toasterHoriz.transform.position;
            toasterHoriz.gameObject.SetActive(false);
            toasterVert.gameObject.SetActive(true);
            isHoriz = false;
        }
        else
        {
            toasterHoriz.transform.position = toasterVert.transform.position;
            toasterVert.gameObject.SetActive(false);
            toasterHoriz.gameObject.SetActive(true);
            isHoriz = true;
        }
        canChangeForms = false;
        
        // Initiate cooldown for switching forms. 
        StartCoroutine(Wait(changeCastTime));
    }

    /// <summary>
    /// Creates a cooldown for the ability for the player to switch between
    /// the horizontal and vertical forms of the toaster. 
    /// </summary>
    /// <param name="time">The amount of time to wait before the ability to switch forms is enabled.</param>
    /// <returns></returns>
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        canChangeForms = true;
    }

}
