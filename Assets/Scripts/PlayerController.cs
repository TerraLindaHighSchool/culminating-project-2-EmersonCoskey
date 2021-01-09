using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /*private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerUpStrength = 15.0f;

    public float speed = 5.0f;
    public bool hasPowerup;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup")) {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Player collided with " + collision.gameObject + " with powerup set to " + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine() {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }*/
    public float jumpHeight;
    public float kickForce;
    public float movementSpeed;

    private GameObject ball;
    private Rigidbody ballRb;
    private SphereCollider ballCollider;

    private Rigidbody rb;
    private Animator anim;
    private Vector3 interpolatedVector;
    private Vector3 interpolationTarget;
    private bool isOnGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = transform.Find("RefereeMesh").gameObject.GetComponent<Animator>();
    }

    private void Update() {
        interpolatedVector = Vector3.Slerp(transform.forward, interpolationTarget, 0.05f).normalized;
        Debug.DrawRay(transform.position, interpolatedVector * 10);
        transform.rotation = Quaternion.LookRotation(interpolatedVector, Vector3.up);
        rb.angularVelocity = new Vector3(0, 0, 0);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 input = movementValue.Get<Vector2>();
        Vector3 movementVector = new Vector3(input.x, 0, input.y);

        anim.SetFloat("Speed_f", movementVector.magnitude);
        interpolationTarget = movementVector;
    }

    private void OnJump()
    {
        if (isOnGround)
        {
            anim.SetTrigger("Jump_trig");
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Force);
        }
    }

    private void OnKick()
    {
        Debug.Log("stuff");
        Debug.Log(transform.Find("Ball"));
        if (transform.Find("Ball") != null)
        {
            ball.transform.parent = null;
            ballRb.isKinematic = false;
            ballRb.AddForce((ball.transform.position - transform.position) * kickForce, ForceMode.Impulse);

            ballCollider.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Ball"))
        {
            ball = collision.collider.gameObject;
            ball.transform.parent = gameObject.transform;
            ball.transform.position = transform.position + transform.forward * 1.5f;

            ballRb = ball.GetComponent<Rigidbody>();
            ballRb.isKinematic = true;

            ballCollider = ball.GetComponent<SphereCollider>();
            ballCollider.enabled = false;
        }

        if (collision.collider.gameObject.CompareTag("Floor")) isOnGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Floor")) isOnGround = false;
    }
}
