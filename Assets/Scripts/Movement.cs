using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveScale;
    private Vector3 direction;
    private RaycastHit[] hits = new RaycastHit[0];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Handle movement of the player unit
    // Call every FixedUpdate
    void TrackMove()
    {
        direction = Vector3.zero;
        
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey("d")  || Input.GetKey("right"))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey("s")  || Input.GetKey("down"))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey("a")  || Input.GetKey("left"))
        {
            direction += Vector3.left;
        }
        
        transform.Translate(direction.normalized * moveScale);
    }

    // Handles partial transparency effect when player is behind objects
    // Call every FixedUpdate
    void TransparentCheck()
    {
        RaycastHit[] newHits = Physics.RaycastAll(transform.position, cameraTransform.position-transform.position);
        HashSet<RaycastHit> newHitsSet = new HashSet<RaycastHit>(newHits);

        foreach (RaycastHit hit in hits)
        {
            if (!newHitsSet.Contains(hit))
            {
                Material mat = hit.transform.gameObject.GetComponent<Renderer>().material;
                mat.color = new Color(1,1,1,1);
            }
        }

        foreach (RaycastHit hit in newHits)
        {
            Material mat = hit.transform.gameObject.GetComponent<Renderer>().material;
            mat.color = new Color(1,1,1,0.25f);
        }

        hits = newHits;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // FixedUpdate for physics based code
    void FixedUpdate()
    {
        TrackMove();
        // TransparentCheck();
    }
}
