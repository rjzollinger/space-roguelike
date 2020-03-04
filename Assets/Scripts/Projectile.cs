﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifespan;
    public int damage;
    public ProjectileType type;

    public enum ProjectileType {
        Enemy,
        Player
    }

    void Start()
    {
        Destroy(gameObject, lifespan);
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject target = collider.gameObject;
        if (collider.gameObject.tag == "Player" && type == ProjectileType.Enemy) {
            Manager.UpdateHealth(-damage);
            Destroy(gameObject);
        } else if (collider.gameObject.tag == "Enemy" && type == ProjectileType.Player) {
            Debug.Log("Enemy hit");
        }
    }
}
