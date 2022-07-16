using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 100f;
    [SerializeField] float lifetime = 5f;
    Vector2 direction;
    private Rigidbody2D rb;
    Vector3 lastVelocity;
    bool started = false;

    public int value; // Goes from 1 to 6

    // Start is called before the first frame update
    private void OnEnable() 
    {
        rb = GetComponent<Rigidbody2D>();

        // For now the value of the bullet will be random, we'll decide if we want it to depend from other things
        value = Random.Range(1, 6);

        StartCoroutine(WaitAndDisable());

    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;

        MoveTowardsTarget();
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void MoveTowardsTarget()
    {
        if(direction != null && !started)
        {
            rb.AddForce(new Vector2(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime));
            started = true;
            //GetComponent<Rigidbody2D>().velocity = direction * speed * Time.deltaTime;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.layer);
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = direction * Mathf.Max(speed, 0f);
    }

}
