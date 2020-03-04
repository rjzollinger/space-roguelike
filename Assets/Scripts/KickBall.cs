using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBall : MonoBehaviour
{
    public Rigidbody ballProjectile;
    private Vector3 spawnOffset = new Vector3(1.25f,0,0);
    //public float ballSpeed = 2000;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("space") && Manager.existingBalls < Manager.maxBalls) {
                Rigidbody ball = Instantiate(ballProjectile, transform.position + spawnOffset, Quaternion.identity);
                //Ray direction = Camera.main.ScreenPointToRay(Input.mousePosition);
                Rigidbody rb = ball.GetComponent<Rigidbody>();
                rb.AddForce(12,2.5f,12, ForceMode.VelocityChange);
                //ball.velocity = transform.forward * ballSpeed;
                Manager.existingBalls++;
            }
        }
}
