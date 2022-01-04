using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioSource levelCompleted;
    [SerializeField] public AudioSource playerJumping;
    [SerializeField] public AudioSource playerDamaged;
    [SerializeField] public AudioSource playerDeath;
    [SerializeField] public AudioSource enemyDamaged;

    public static AudioManager Instance { get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
