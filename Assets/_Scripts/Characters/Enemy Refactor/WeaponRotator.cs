using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    public void Rotate2Target(Vector3 targetPos) {
        Vector3 difference = targetPos - transform.position;
        difference.Normalize();
        float rotationAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }
}
