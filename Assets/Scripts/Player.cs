using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxAccel = 2000f;
    public float maxSpeed = 10f;
    public GameObject bullet;
    public float bulletSpawnYOffset = 0.8f;
    private Vector2 velocity;
    private Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // spawn player bullet on left-click
        if (Input.GetMouseButtonDown(0))
        {
            shootBullet();
        }

        // accelerate left
        if (Input.GetKey(KeyCode.A))
        {
            velocity = new Vector2(-maxAccel * Time.deltaTime, 0f);
        }

        // accelerate right
        else if (Input.GetKey(KeyCode.D))
        {
            velocity = new Vector2(maxAccel * Time.deltaTime, 0f);
        }

        else
        {
            velocity = Vector2.zero;
        }

        // Limit the max speed
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
    }

    private void shootBullet()
    {
        GameObject newBullet = Instantiate(bullet);
        newBullet.transform.parent = null;
        newBullet.transform.position = new Vector3(transform.position.x, transform.position.y + bulletSpawnYOffset, transform.position.z);
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = velocity;
    }
}
