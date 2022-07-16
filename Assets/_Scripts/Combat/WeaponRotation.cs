using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = transform.parent.GetComponent<PlayerController>();

        playerController.onMousePositionUpdate += OnMousePositionUpdate;
    }

    private void OnMousePositionUpdate(Vector3 mousePos)
    {
        Vector3 difference = mousePos - transform.position;
        difference.Normalize();
        float rotationAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }
}
