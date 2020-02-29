using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBall : MonoBehaviour
{
    public GameObject player;
    public Rigidbody ballProjectile;
    private Vector3 spawnOffset = new Vector3(1,0,0);
    public float ballSpeed = 5;
    private int existingBalls = 0;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("space") && existingBalls < 30) {
                Rigidbody ball = Instantiate(ballProjectile, player.transform.position + spawnOffset, Quaternion.identity);
                ball.velocity = transform.forward * ballSpeed;
                existingBalls++;
            }
        }

        //void OnCollision
}
