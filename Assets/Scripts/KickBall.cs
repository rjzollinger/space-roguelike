using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBall : MonoBehaviour
{
    public Rigidbody ballProjectile;
    public Camera mainCam;
    public float projectileForce;
    private float offset = 1.5f;
    private Vector3 spawnOffset = new Vector3(1.25f, 0, 0);
    //public float ballSpeed = 2000;

    // Executes a kick by shooting a projectile toward the clicked location
    void Kick()
    {
        // Cast a ray from the camera into the scene
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit)) {
            // Get the direction to the clicked location
            Vector3 forceVector = (hit.point - transform.position).normalized;

            // Spawn projectile and add force to it
            Rigidbody ball = Instantiate(ballProjectile, transform.position + offset * forceVector, Quaternion.identity);
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.AddForce(projectileForce * forceVector, ForceMode.VelocityChange);

            // Increment the ball counter
            Manager.UpdateBallCount(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown("space") && Manager.existingBalls < Manager.maxBalls)
        // {
        //     Rigidbody ball = Instantiate(ballProjectile, transform.position + spawnOffset, Quaternion.identity);
        //     //Ray direction = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     Rigidbody rb = ball.GetComponent<Rigidbody>();
        //     rb.AddForce(12, 2.5f, 12, ForceMode.VelocityChange);
        //     //ball.velocity = transform.forward * ballSpeed;
        //     Manager.existingBalls++;
        // }
        if (Input.GetMouseButtonDown(0) && Manager.existingBalls < Manager.maxBalls && Manager.GetGameActiveStatus())
        {
            Kick();
        }
    }
}
