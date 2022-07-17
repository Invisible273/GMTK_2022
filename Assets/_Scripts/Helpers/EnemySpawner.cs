using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] BoxCollider2D field;
    [SerializeField] float timeBetweenSpawns = 2f;
    [Range(1,10)]
    [SerializeField] int offset; 
    Vector2 fieldCenter;
    float[] rX;

    float timeSinceDifficultyIncrease;

    void Start()
    {
        rX = new float[2] { -offset, field.size.x + offset};
        fieldCenter = new Vector2(field.size.x *0.5f, field.size.y * 0.5f);     
        timeSinceDifficultyIncrease = 0;   
        StartCoroutine(SpawnerRoutine());
    }

    void Update()
    {
        //Timer that increases spawn rate
        timeSinceDifficultyIncrease += Time.deltaTime;
        if (timeSinceLevelStart >= 100.0f)
        {
            if (timeBetweenSpawns >= 1.0f)
            {
                timeBetweenSpawns -= 0.1f;
            }
            timeSinceLevelStart = 0;
        }
    }

    private IEnumerator SpawnerRoutine()
    {
        while(true)
        {
            Spawn();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        

    }
    
    private void Spawn()
    {
        Instantiate(enemyPrefab, GenerateSpawnPosition(), Quaternion.identity);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float rY = UnityEngine.Random.Range(0-offset, field.size.y+offset);
        
        if(rY > field.size.y || rY < 0)
        {
            float randomX = UnityEngine.Random.Range(0 - offset, field.size.x + offset);

            return new Vector2(randomX, rY);
        }        

        else if(rY <= field.size.y)
        {
            float randomX =rX[UnityEngine.Random.Range(0, rX.Length)];
            
            return new Vector2(randomX, rY);
        }

        return fieldCenter;

    }
}
