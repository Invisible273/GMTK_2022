using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    [SerializeField] float minDistanceFromPlayer = 5f;
    [SerializeField] float shootInterval = 2f;
    [SerializeField] LayerMask fieldLayer;
    Coroutine routine;
    GameObject player = null;
    Collider2D myCol;

    Vector2 directionToPlayer;

    void Start()
    {
        myCol = GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player");      
        StartCoroutine(AimAndShoot());
    } 

    void Update()
    {
        directionToPlayer = GetDirectionToPlayer();
        EnemyMovement();
    }

    private Vector2 GetDirectionToPlayer()
    {
        if(player)
        {
        return player.transform.position - transform.position;
        }
        else return Vector2.zero;
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
        if(CanShoot(fieldLayer))
        {
            GetComponent<Shooter>().Shoot(shootDir);
        }
        
        yield return new WaitForSeconds(shootInterval);
        }

    }

    private bool CanShoot(LayerMask layer)
    {
        int fLayer = layer.value;
       
        return myCol.IsTouchingLayers(fLayer);
       
    }
}
