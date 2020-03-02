using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Public Reference")]
    public Rigidbody projectile;

    [Header("Movement/Turn Speed")]
    public float moveScale;
    public float rotationScale;

    [Header("Attributes/Behavior")]
    public int health;
    public float aggroDistance;
    public float followDistance;
    public float projectileVelocity;
    public float shootInterval;

    private Transform player;
    private float fireTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Aggro(float dist) {
        fireTimer += Time.deltaTime;
        Vector3 lookDirection = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection);
    
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            rotation,
            rotationScale
        );

        if (fireTimer > shootInterval) {
            FireProjectile(player.position, rotation);
            fireTimer = 0;
        }

        if (dist > followDistance) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                moveScale
            );
        }
    }

    void FireProjectile(Vector3 target, Quaternion rotation) {
        Rigidbody newProjectile = Instantiate(projectile, transform.position, rotation);
        newProjectile.velocity = (target - transform.position).normalized * projectileVelocity;
    }

    void Idle() {
        transform.Rotate(0,1,0);
    }

    void FixedUpdate()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        if (dist < aggroDistance) {
            Aggro(dist);
        } else {
            Idle();
        }
    }
}
