using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifespan;
    void Start()
    {
        Destroy(gameObject, lifespan);
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject target = collider.gameObject;
        if (collider.gameObject.tag == "Player") {
            Debug.Log("Player hit");
            Destroy(gameObject);
        }
    }
}
