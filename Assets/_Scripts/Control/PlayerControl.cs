using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float shootInterval;
    Mover mover;
    Coroutine routine;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        mover = GetComponent<Mover>();        
    }

    // Update is called once per frame
    void Update()
    {       
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            routine = StartCoroutine(ShootAtMousePos());
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopCoroutine(routine);
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        mover.MoveBody(horizontal, vertical);
    }

    private IEnumerator ShootAtMousePos()
    {
        while(true)
        {
        Vector3 direction = cam.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono) - transform.position;
        GetComponent<Shooter>().Shoot(direction);
        yield return new WaitForSeconds(shootInterval);
        }

    }
}
