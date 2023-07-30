using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxAccel = 2000f;
    public float maxSpeed = 10f;
    public float bulletSpawnYOffset = 0.8f;
    private Vector2 velocity;
    private Rigidbody2D myRigidbody;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // spawn player bullet on left-click or pressing spacebar
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            shootBullet();
        }

        int moveDirection = Mathf.FloorToInt(Input.GetAxisRaw("Horizontal"));
        if (moveDirection == -1)
        {
            velocity = new Vector2(-maxAccel * Time.deltaTime, 0f);
        }
        else if (moveDirection == 1)
        {
            velocity = new Vector2(maxAccel * Time.deltaTime, 0f);
        }
        else {
            velocity = Vector2.zero;
        }

        // Limit the max speed
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
    }

    private void shootBullet()
    {
        GameObject bullet = BulletPool.SharedInstance.GetPooledObject();
        if (bullet == null) return;
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y + bulletSpawnYOffset, transform.position.z);
        bullet.SetActive(true);
        audioSource.Play();
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = velocity;
    }
}
