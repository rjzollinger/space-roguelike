using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileType type;
    public float lifespan;
    public int damage;
    public float pickupDistance;
    public float pickupMoveScale;

    private AudioSource hitSound;
    private Transform player;
    private float initializationTime;

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
        player = GameObject.Find("Player").transform;
        initializationTime = Time.timeSinceLevelLoad;
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
        } else if (collider.gameObject.tag != "Enemy" && collider.gameObject.tag != "Floor" && type == ProjectileType.Enemy) {
            Destroy(gameObject);
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

    void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            pickupMoveScale
        );
    }

    void FixedUpdate()
    {
        if (type == ProjectileType.Player)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            if (dist < pickupDistance)
            {
                float timeSinceInitialization = Time.timeSinceLevelLoad - initializationTime;
                if (timeSinceInitialization > 1)
                {
                    MoveTowardsPlayer();
                }
            }
        }
    }
}
