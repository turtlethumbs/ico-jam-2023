using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Monster monster;
    public float minCooldown = 3f;
    public float maxCooldown = 5f;
    private float cooldown = 0f;
    private float nextSpawnTime;
    private Vector2 spawnZoneDimensions;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        generateNextSpawnTime();

        spawnZoneDimensions = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    void Update()
    {
        cooldown += Time.deltaTime * gameManager.level;
        if (cooldown >= nextSpawnTime)
        {
            generateNextSpawnTime();
            cooldown = 0;
            spawnMonster();
        }
    }

    void generateNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minCooldown, maxCooldown);
    }

    void spawnMonster()
    {
        Monster theMonster = Instantiate(monster);
        theMonster.transform.parent = null;
        Vector2 spawnPosition = new Vector2(Random.Range(-spawnZoneDimensions.x/2, spawnZoneDimensions.x/2), transform.position.y);
        theMonster.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0f);
    }
}
