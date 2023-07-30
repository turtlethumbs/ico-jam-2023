using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int moveDirection = 0;
    public float walkSpeed = 5f;
    public float maxFallSpeed = 100f;
    public int maxHealth = 3;
    public bool isFalling = true;
    public string damagerTagName = "player_bullet";
    public int giveMoney = 1;
    private int health;
    private Player player;
    private float velocity;
    private Rigidbody2D myRigidbody;
    private DamageFlash damageFlash;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.Log("Monster spawned but was unable to find the Player!");
        }
        myRigidbody = GetComponent<Rigidbody2D>();
        damageFlash = GetComponent<DamageFlash>();
        ResetHealth();
    }

    private void Update()
    {
        if (health <= 0)
        {
            ResetHealth();
            damageFlash.ResetFlash();
            gameObject.SetActive(false);
        }

        if (player == null) return;

        // Update the movement direction based on player location
        moveDirection = (transform.position.x < player.transform.position.x) ? 1 : -1;

        // Go after the player
        velocity = (!isFalling) ? velocity + (walkSpeed * Time.deltaTime) * moveDirection : 0;        
    }

    private void FixedUpdate()
    {
        float fallSpeed = Mathf.Clamp(myRigidbody.velocity.y, -maxFallSpeed, maxFallSpeed);
        myRigidbody.velocity = new Vector2(velocity, fallSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isFalling = false;
            transform.rotation = Quaternion.identity;
            myRigidbody.excludeLayers = LayerMask.GetMask("Monster");
            myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        }

        if (collision.gameObject.tag == damagerTagName)
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            health -= bullet.damage;
            health = Mathf.Clamp(health, 0, maxHealth);
            damageFlash.TakeDamage();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isFalling = true;
        }
    }

    private void ResetHealth()
    {
        health = maxHealth;
    }
}
