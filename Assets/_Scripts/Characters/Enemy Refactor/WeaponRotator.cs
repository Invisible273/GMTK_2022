using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    private float lastRotationAngle = 0.0f;

    public void Rotate2Target(Vector3 targetPos) {
        Vector3 difference = targetPos - transform.position;
        difference.Normalize();
        float rotationAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }

    public void Rotate2TargetSnap(Vector3 targetPos) {
        Vector3 difference = targetPos - transform.position;
        difference.Normalize();
        float targetAngle = 0;
        if (difference.y <= difference.x)
        {
            if (difference.y >= -difference.x)
                targetAngle = 0;
            else
                targetAngle = 270;
        }
        else
        {
            if (difference.y <= -difference.x)
                targetAngle = 180;
            else
                targetAngle = 90;
        }
        if (targetAngle != lastRotationAngle)
        {
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                transform.eulerAngles.y,
                targetAngle
            );
        }
    }
}
