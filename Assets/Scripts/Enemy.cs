using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveScale;
    public float rotationScale;
    public float aggroDistance;
    public float followDistance;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Aggro(float dist) {
        Vector3 desired = player.position;
        Vector3 lookDirection = player.position - transform.position;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(lookDirection),
            rotationScale
        );

        if (dist > followDistance) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                desired,
                moveScale
            );
        }
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
