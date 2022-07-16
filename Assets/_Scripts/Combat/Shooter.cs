using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] ObjectPool myPool;
    [SerializeField] GameObject tempProjectile;
     

   

    public void Shoot(Vector3 direction)
    {
        // I use Instantiate() just for testing, feel free to reuse pooling when the pool object will get created
        //GameObject projectile = myPool.GetPooledObject();
        //GameObject projectile = Instantiate(tempProjectile);
        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject();
        
        if (projectile != null)
        {
            projectile.transform.position = transform.position + direction.normalized;
            projectile.SetActive(true);
            projectile.GetComponent<Projectile>().SetDirection(direction);
        }
        

    }
}
