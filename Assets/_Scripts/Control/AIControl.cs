using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    [SerializeField] float minDistanceFromPlayer = 5f;
    [SerializeField] float shootInterval = 2f;
    Coroutine routine;
    GameObject player = null;

    Vector2 directionToPlayer;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");      
        StartCoroutine(AimAndShoot());
    }
    
    // Update is called once per frame
    void Update()
    {
        directionToPlayer = GetDirectionToPlayer();
        EnemyMovement();


    }

    private Vector2 GetDirectionToPlayer()
    {
        return player.transform.position - transform.position;
    }

    private void EnemyMovement()
    {
        if(Vector2.Distance(player.transform.position, transform.position) > minDistanceFromPlayer)
        {
            Vector2 nDir = directionToPlayer.normalized;
            GetComponent<Mover>().MoveBody(nDir.x, nDir.y);
        }
        else
        {
            GetComponent<Mover>().Stop();            
        }
       
    }

    private IEnumerator AimAndShoot()
    {
        while(true)
        {
        Vector2 shootDir = GetDirectionToPlayer();
        GetComponent<Shooter>().Shoot(shootDir);
        yield return new WaitForSeconds(shootInterval);
        }

    }
}
