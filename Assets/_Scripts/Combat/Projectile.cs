using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 100f;
    [SerializeField] float lifetime = 5f;
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

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            print("Hit Player!");
        }
        if(other.gameObject.tag == "Enemy")
        {
            print("Hit Enemy!");
        }
    }
}
