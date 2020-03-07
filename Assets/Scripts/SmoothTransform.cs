using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothTransform : MonoBehaviour
{
    private Vector3 targetPosition;
    private float smoothFactor;

    public void UpdatePosition(Vector3 target, float smoothness) {
        targetPosition = target;
        smoothFactor = smoothness;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothFactor);
    }
}
