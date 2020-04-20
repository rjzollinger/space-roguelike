using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InWorldUIScript : MonoBehaviour
{
    // The target transform for this UI element to follow
    public Transform target;
    // The canvas object to place this UI element into
    // private Transform canvas;
    // The camera to use for projection
    public Camera mainCamera;

    // Offset from the target in UI coordinates (only X and Y)
    public Vector3 offset;

    // Slider attached with the UI
    public Slider infoSlider;

    // Start is called before the first frame update
    void Start()
    {
        this.mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Set up the panel with a target Transform and UI offset
    public void SetTransformAndOffset(Transform target, Vector3 offset){
        this.target = target;
        this.offset = offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCamera.WorldToScreenPoint(target.position) + offset;
    }
}
