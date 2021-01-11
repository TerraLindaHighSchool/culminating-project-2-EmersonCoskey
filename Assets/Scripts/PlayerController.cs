using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
        if (collision.gameObject.CompareTag("Ball"))
        {
            ball = collision.gameObject;
            ball.transform.parent = gameObject.transform;
            ball.transform.position = transform.position + transform.forward * 1.5f + transform.up * -1;

            ballRb = ball.GetComponent<Rigidbody>();
            ballRb.isKinematic = true;

            ballCollider = ball.GetComponent<SphereCollider>();
            ballCollider.enabled = false;
        }

        if (collision.gameObject.CompareTag("Floor")) isOnGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) isOnGround = false;
    }
}
