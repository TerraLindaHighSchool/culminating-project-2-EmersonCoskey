using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallImpactController : MonoBehaviour
{
    public float hitForceMultiplier;
    public float minSpeed;
    public float powerupDuration;
    private Rigidbody rb;
    private bool isPowerupActive;
    private Vector3 originalScale;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (isPowerupActive)
        {
            transform.localScale = Vector3.Slerp(transform.localScale, originalScale * 3.0f, 0.05f);
        }
        else
        {
            transform.localScale = Vector3.Slerp(transform.localScale, originalScale, 0.05f);
        }
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
        if (collider.gameObject.CompareTag("Powerup"))
        {
            isPowerupActive = true;
            rb.AddForce(Vector3.up * 2, ForceMode.Impulse);
            Destroy(collider.gameObject);
            StartCoroutine("WaitForPowerupCooldown");
        }
    }

    IEnumerator WaitForPowerupCooldown()
    {
        yield return new WaitForSeconds(12);
        isPowerupActive = false;
    }

    public bool GetIsPowerupActive()
    {
        return isPowerupActive;
    }
}
