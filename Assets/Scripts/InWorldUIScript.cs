using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWorldUIScript : MonoBehaviour
{
    // The target transform for this UI element to follow
    public Transform target;
    // The canvas object to place this UI element into
    // private Transform canvas;
    // The camera to use for projection
    public Camera mainCamera;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCamera.WorldToScreenPoint(target.position) + offset;
    }
}
