using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallImpactController : MonoBehaviour
{
    public float hitForceMultiplier;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && rb.velocity.magnitude > 10.0f)
        {
            Rigidbody EnemyRb = collision.gameObject.GetComponent<Rigidbody>();

            EnemyRb.AddForce(rb.velocity * hitForceMultiplier, ForceMode.Impulse);
        }
    }
}
