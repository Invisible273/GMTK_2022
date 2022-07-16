using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 100f;
    [SerializeField] float lifetime = 5f;
    [SerializeField] Characters target;
    [SerializeField] float damage = 10f;
    Vector2 direction;
    // Start is called before the first frame update
    private void OnEnable() 
    {
        StartCoroutine(WaitAndDisable());        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void MoveTowardsTarget()
    {
        if(direction != null)
        {
            GetComponent<Rigidbody2D>().velocity = direction * speed * Time.deltaTime;
        }
    }

    IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == target.ToString())
        {
            other.GetComponent<Health>().GetDamaged(damage);
           
        }
        
    }

}
