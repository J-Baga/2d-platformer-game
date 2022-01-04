using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform playerHoriz, playerVert;

    public Vector3 offset;

    private void FixedUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        if (playerHoriz.gameObject.activeSelf)
        {
            transform.position = playerHoriz.position + offset;
        }
        else if (playerVert.gameObject.activeSelf)
        {
            transform.position = playerVert.position + offset;
        }
    }
}
