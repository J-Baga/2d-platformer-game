using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private AudioManager playerStateAudio;
    private LevelManager levelManage;
    private LevelUIManager levelUI;
    [SerializeField] private LayerMask goal;
    [SerializeField] private GameObject toasterHoriz, toasterVert;

    private float damagedAnimationTime = 1f;
    private float objectSpawnPosY;
    private bool isInvincible;
    public bool isDead;

    private void Start()
    {
        SetColor(PlayerManager.color);
        SetHealth(PlayerManager.difficulty);
        levelUI = GameObject.Find("LevelUIManager").GetComponent<LevelUIManager>();
        playerStateAudio = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        levelManage = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        isDead = false;
        objectSpawnPosY = gameObject.transform.position.y;
        levelUI.healthUpdate();
    }

    private void FixedUpdate()
    {
        // Check if player is on the goal
        if (reachedGoal(toasterHoriz.GetComponent<CircleCollider2D>()) || reachedGoal(toasterVert.GetComponent<CircleCollider2D>())){
            levelManage.CompletedLevel();
            this.enabled = false;
        }
        if (toasterHoriz.transform.position.y < objectSpawnPosY - 50 || toasterVert.transform.position.y < objectSpawnPosY - 50)
        {
            TakeDamage(PlayerManager.health);
            this.enabled = false;
        }

    }

    public void TakeDamage (int damage)
    {
        // Player has 1 second of invinsibility after taking damage
        if (isInvincible)
        {
            return;
        }
        toasterHoriz.GetComponent<Animator>().SetBool("wasDamaged", true);
        toasterVert.GetComponent<Animator>().SetBool("wasDamaged", true);
        playerStateAudio.playerDamaged.Play();
        PlayerManager.health -= damage;
        levelUI.healthUpdate();
        // Player dies after reaching 0 health
        if (PlayerManager.health <= 0)
        {
            isDead = true;
            Die();
        }
        StartCoroutine(LetDamagedAnimRun(damagedAnimationTime));
    }

    public void getCollectible()
    {
        PlayerManager.numCollectibles += 0.5f;
        levelUI.collectibleUpdate();
    }

    public void increaseScore(int score)
    {
        PlayerManager.score += score;
        levelUI.scoreUpdate();
    }

    private void Die()
    {
        playerStateAudio.playerDeath.Play();
        levelManage.FailedLevel();
        isDead = true;
        toasterHoriz.GetComponent<Animator>().SetTrigger("death");
        toasterVert.GetComponent<Animator>().SetTrigger("death");
        toasterHoriz.GetComponent<BoxCollider2D>().enabled = false;
        toasterVert.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("GoGameOver", 3.5f);
    }

    private void GoGameOver()
    {
        levelManage.GameOver();
    }

    IEnumerator LetDamagedAnimRun(float time)
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        toasterHoriz.GetComponent<Animator>().SetBool("wasDamaged", false);
        toasterVert.GetComponent<Animator>().SetBool("wasDamaged", false);
        isInvincible = false;
    }

    private bool reachedGoal(CircleCollider2D coll)
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, goal);
    }

    private void SetHealth(string difficulty)
    {
        switch (difficulty)
        {
            case "Normal":
                PlayerManager.health = 20;
                break;

            case "Easy":
                PlayerManager.health = 30;
                break;

            case "Hard":
                PlayerManager.health = 10;
                break;

            case "Expert":
                PlayerManager.health = 5;
                break;

            case "Chaotic":
                PlayerManager.health = 1;
                break;
        }
    }

    private void SetColor(string color)
    {
        switch (color)
        {
            case "Red":
                toasterHoriz.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                toasterVert.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;

            case "Blue":
                toasterHoriz.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                toasterVert.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;

            case "Green":
                toasterHoriz.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                toasterVert.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;

            case "Magenta":
                toasterHoriz.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                toasterVert.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
                break;

            case "Cyan":
                toasterHoriz.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                toasterVert.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                break;

            case "Yellow":
                toasterHoriz.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                toasterVert.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;

            case "Black":
                toasterHoriz.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                toasterVert.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                break;
        }
    }
}
