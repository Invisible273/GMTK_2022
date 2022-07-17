using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float minDistanceFromPlayer = 5f;
    [SerializeField] float shootInterval = 2f;
    [SerializeField] LayerMask fieldLayer;
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

            MoveInDirection(nDir);
        }
        else
        {
            MoveInDirection(Vector2.zero);    
        }  
    }

    private IEnumerator AimAndShoot()
    {
        while(true)
        {
            Vector2 shootDir = GetDirectionToPlayer();
            if(CanShoot(fieldLayer))
            {
                RotateToTarget(player.transform.position); 
                ShootInDirection(shootDir);
            }
        
            yield return new WaitForSeconds(shootInterval);
        }
    }

    private bool CanShoot(LayerMask layer)
    {
        int fLayer = layer.value;
       
        return myCol.IsTouchingLayers(fLayer);
    }

    public delegate void ToTarget2D(Vector2 targetDir);
    public event ToTarget2D onMove2Target;
    public void MoveInDirection(Vector2 targetDir) 
    {
        onMove2Target?.Invoke(targetDir);
    }

    public event ToTarget2D onShoot2Target;
    public void ShootInDirection(Vector2 targetDir) 
    {
        onShoot2Target?.Invoke(targetDir);
    }

    public delegate void ToTarget(Vector3 targetDir);
    public event ToTarget onRotate2Target;
    public void RotateToTarget(Vector3 targetDir) 
    {
        onRotate2Target?.Invoke(targetDir);
    }
}
