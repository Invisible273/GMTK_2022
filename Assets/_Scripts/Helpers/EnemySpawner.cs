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

    // Start is called before the first frame update
    void Start()
    {
        rX = new float[2] { -offset, field.size.x + offset};
        fieldCenter = new Vector2(field.size.x *0.5f, field.size.y * 0.5f);        
        StartCoroutine(SpawnerRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
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