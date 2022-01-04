using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int health;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float travelWidth;
    [SerializeField] private Transform tfPlayerHoriz, tfPlayerVert, detectSpawn;
    [SerializeField] float idleTime;
    [SerializeField] int aggroDistance;
    [SerializeField] private GameObject aggroSprite;

    [SerializeField] private GameObject explosionEffect;

    private Player player;
    private AudioManager enemyStateAudio;

    private GameObject enem;
    private GameObject aggroSpriteClone;
    private Animator enemAnimation;
    private float damagedAnimationTime = 1f;
    private float enemyMinPos, enemyMaxPos;
    private float horizontalInput;
    private bool shouldStop, isFacingLeft;
    public bool aggro; 

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        aggroSprite.SetActive(true);
        enem = gameObject;
        enemAnimation = enem.GetComponent<Animator>();
        enemyMinPos = enem.GetComponent<Transform>().position.x - travelWidth;
        enemyMaxPos = enem.GetComponent<Transform>().position.x + travelWidth;
        horizontalInput = -0.5f;
        shouldStop = false;
        isFacingLeft = true;
        aggro = false;

        enemyStateAudio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void FixedUpdate()
    {
        // Make aggro detection sprite follow enemy.
        if(aggroSpriteClone != null)
        {
            aggroSpriteClone.transform.position = new Vector2(detectSpawn.position.x + horizontalInput * moveSpeed
            * Time.deltaTime, detectSpawn.transform.position.y);
        }

        // Move when done being idle (cooldown time).
        if (shouldStop == false)
        {
            enemAnimation.SetBool("isMoving", true);
            transform.position = new Vector2(transform.position.x + horizontalInput * moveSpeed * Time.deltaTime, transform.position.y);
        }

        // Stop moving when it gets to one of its "stop points."
        if ((transform.position.x <= enemyMinPos || transform.position.x >= enemyMaxPos) && shouldStop == false)
        {
            Idle();
            horizontalInput *= -1;
        }

        // Check for aggro on either forms of the player.
        if (tfPlayerHoriz.gameObject.activeSelf)
        {
            aggro = IsAggro(tfPlayerHoriz) && player.isDead == false;
        }
        else
        {
            aggro = IsAggro(tfPlayerVert) && player.isDead == false;
        }

        // If aggro, create aggro sprite to let player know he/she has been detected by enemy.
        if (aggro == true && aggroSpriteClone == null)
        {
            aggroSpriteClone = Instantiate(aggroSprite, detectSpawn.position, detectSpawn.rotation);
        }
        // Destroy aggro sprite if player is not in aggro range anymore.
        else if (aggro == false && aggroSpriteClone != null)
        {
            Destroy(aggroSpriteClone);
        }
    }

    public void TakeDamage (int damage)
    {
        StartCoroutine(LetDamagedAnimRun(damagedAnimationTime));
        enemyStateAudio.enemyDamaged.Play();
        enemAnimation.SetBool("wasDamaged", true);
        health -= damage;
        if (health <= 0)
        {
            player.increaseScore(score);
            Die();
        }
    }

    private void Die()
    {
        GameObject explosionClone = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(aggroSpriteClone);
        Destroy(explosionClone, 1.0f);
    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Idle()
    {
        enemAnimation.SetBool("isMoving", false);
        shouldStop = true;
        StartCoroutine(WaitThenTurn(idleTime));
    }

    IEnumerator WaitThenTurn(float time)
    {
        yield return new WaitForSeconds(time);
        Flip();
        shouldStop = false;
    }

    private bool IsAggro(Transform tf)
    {
        float distToPlayer = Vector2.Distance(transform.position, tf.position);
        float vertDistToPlayer = Mathf.Abs(transform.position.y - tf.position.y);
        float horizDistToPlayer = Mathf.Abs(transform.position.x - tf.position.x);

        // Player must be in aggro range to be considered aggro'd.
        if ((distToPlayer > aggroDistance && horizDistToPlayer > aggroDistance) || vertDistToPlayer > 0.50)
        {
            return false;
        }

        // Makes sure enemy is facing player when the player is in aggro range to properly detect aggro.
        if (isFacingLeft == true)
        {
            if (tf.position.x <= transform.position.x)
            {
                return true;
            }
        }
        else if (isFacingLeft == false)
        {
            if (tf.position.x >= transform.position.x)
            {
                return true;
            }
        }
        return false;
    }
    
    IEnumerator LetDamagedAnimRun(float time)
    {
        yield return new WaitForSeconds(time);
        enemAnimation.SetBool("wasDamaged", false);
    }
}
