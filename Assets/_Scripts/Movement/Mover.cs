using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mover : MonoBehaviour
{
    [SerializeField] float speedMod = 1000f;
    int startingRoll = 1;
    Rigidbody2D mybody;

    
    void Start()
    {
        mybody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //MoveBody();
    }

    public void MoveBody(float horizontal, float vertical)
    {        
        mybody.velocity = new Vector2(horizontal * speedMod * Time.deltaTime, vertical * speedMod * Time.deltaTime);
    }

    public void Stop()
    {
        mybody.velocity = Vector2.zero;
    }

    private void RollTheDice(int previousRoll)
    {
        

    }


}
