using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallImpactController : MonoBehaviour
{
    public float hitForceMultiplier;
    public float minSpeed;
    private Rigidbody rb;
    private bool isPowerupActive;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && rb.velocity.magnitude > minSpeed)
        {
            Rigidbody EnemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Enemy enemyController = collision.gameObject.GetComponent<Enemy>();

            EnemyRb.AddForce(rb.velocity * (isPowerupActive ? hitForceMultiplier * 2 : hitForceMultiplier), ForceMode.Impulse); ;
            rb.velocity = rb.velocity * 0.1f;
            enemyController.RagDoll();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        { 
            
        }
    }
}
