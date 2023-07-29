using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourGlass : MonoBehaviour
{
    
    public float maxAngle = 90f;
    public float maxEnergy = 100f;
    public float energyAddition = 10f;
    public float drainEnergyPerSecond = 1f;
    public string damagerTagName = "player_bullet";
    public float countDownTime = 3f;
    public bool isSelfDestructing = false;

    private float angle = 0f;
    private float energy;
    private float selfDestructTimer = 0f;
    private GameManager gameManager;

    void Start() {
        energy = maxEnergy;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        energy -= drainEnergyPerSecond * Time.deltaTime;
        energy = Mathf.Clamp(energy, 0, maxEnergy);
        angle = (energy / maxEnergy) * maxAngle;
        angle = Mathf.Clamp(angle, 0f, maxAngle);
        transform.eulerAngles = new Vector3(0f, 0f, angle);
        if (!isSelfDestructing)
        {
            if (energy <= 0)
                selfDestructTimer += Time.deltaTime;
            else
                selfDestructTimer = 0f;
            if (countDownTime - selfDestructTimer <= 0)
                SelfDestruct();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isSelfDestructing) return;
        if (collision.gameObject.tag == damagerTagName)
        {
            energy += energyAddition;
            energy = Mathf.Clamp(energy, 0, maxEnergy);
        }
    }

    private void SelfDestruct()
    {
        isSelfDestructing = true;
    }
}
