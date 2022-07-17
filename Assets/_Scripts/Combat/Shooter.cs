using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] ObjectPool myPool;
    
    public void Shoot(Vector3 direction)
    {
        GameObject projectile = ObjectPool.SharedInstance.GetPooledObject();
        if (projectile != null)
        {
            projectile.transform.position = transform.position + direction.normalized;
            projectile.SetActive(true);
            projectile.GetComponent<Projectile>().SetDirection(direction);
        }
    }
}
