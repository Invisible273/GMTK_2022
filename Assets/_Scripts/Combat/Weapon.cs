using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Header("Parrying")]
    [SerializeField] float parryTimer = 0.25f;
    public bool isParrying = false;
    [SerializeField] SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(ParryCoroutine());
        }

        if (isParrying)
        {
            spriteRenderer.color = new Color(255, 0, 0, 255);
        }
        else
        {
            spriteRenderer.color = new Color(255, 0, 255, 255);
        }
    }

    private IEnumerator ParryCoroutine()
    {
        isParrying = true;
        gameObject.layer = LayerMask.NameToLayer("Parry");
        yield return new WaitForSeconds(parryTimer);
        gameObject.layer = LayerMask.NameToLayer("Default");
        isParrying = false;
    }
}
