using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDisappear : MonoBehaviour
{
    public Rigidbody ball;
    public Rigidbody player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Manager.existingBalls--;
        }
    }
}
