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
    public int timeUntilSelfDestruct;
    public float countDownTime = 3f;
    public bool isCountingDown = false;
    public bool isSelfDestructing = false;
    private float angle = 0f;
    private float energy;
    private float selfDestructTimer = 0f;

    void Start() {
        isCountingDown = false;
        energy = maxEnergy;
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
            {
                isCountingDown = true;
                selfDestructTimer += Time.deltaTime;
            }
            else
            {
                isCountingDown = false;
                selfDestructTimer = 0f;
                timeUntilSelfDestruct = (int)countDownTime;
            }
            timeUntilSelfDestruct = Mathf.CeilToInt(countDownTime - selfDestructTimer);
            if (timeUntilSelfDestruct <= 0)
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
