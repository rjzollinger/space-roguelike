using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifespan;
    public int damage;
    public ProjectileType type;
    private AudioSource hitSound;

    public enum ProjectileType {
        Enemy,
        Player
    }

    void Start()
    {
        if (lifespan >= 0)
        {
            Destroy(gameObject, lifespan);
        }

        hitSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject target = collider.gameObject;
        if (collider.gameObject.tag == "Player" && type == ProjectileType.Enemy) {
            //Debug.Log("hit");
            AudioSource.PlayClipAtPoint(hitSound.clip, collider.gameObject.transform.position, 0.1f);
            Manager.UpdateHealth(-damage);
            Destroy(gameObject);
        } else if (collider.gameObject.tag == "Enemy" && type == ProjectileType.Player) {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.UpdateHealth(-damage);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;
        if (collision.gameObject.tag == "Player" && type == ProjectileType.Enemy) {
            Manager.UpdateHealth(-damage);
            Destroy(gameObject);
        } else if (collision.gameObject.tag == "Enemy" && type == ProjectileType.Player) {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.UpdateHealth(-damage);
        }
    }
}
