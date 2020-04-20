using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Public Reference")]
    public Rigidbody projectile;
    public InWorldUIScript enemyPanelPrefab;

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

    [Header("In-World UI Settings")]
    public Vector3 inWorldPanelOffset;
    private InWorldUIScript enemyPanel;

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

    public void UpdateHealth(int amount) {
        if (health > 0) {
            health = Mathf.Max(health + amount, 0);
            enemyPanel.infoSlider.value = health;
        }
        if (health <= 0) {
            Destroy(enemyPanel.gameObject);
            Destroy(gameObject);
        }
    }

    // Sets up in-world panel references
    public void SetupEnemy()
    {
        // Get a reference to the scene manager (for access to the dynamic canvas)
        Manager manager = GameObject.FindWithTag("Manager").GetComponent<Manager>();
        enemyPanel = Instantiate(enemyPanelPrefab, manager.dynamicCanvas);

        // Set the panel target, offset, and slider data
        enemyPanel.SetTransformAndOffset(transform, inWorldPanelOffset);
        enemyPanel.infoSlider.maxValue = health;
        enemyPanel.infoSlider.value = health;
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
