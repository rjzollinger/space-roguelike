using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 desired = target.position + offset;
        Vector3 smoothed = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothing);
        transform.position = smoothed;
        transform.LookAt(new Vector3(transform.position.x, target.position.y, target.position.z));
    }
}
