using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public Vector3 target;
    public GameObject gameControllerObject;

    private GameController gameController;

    private void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            gameController.Score(collider.gameObject);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        Rigidbody otherRb = collider.gameObject.GetComponent<Rigidbody>();
        if (collider.gameObject.CompareTag("Enemy"))
        {
            otherRb.AddForce(otherRb.velocity * -0.05f, ForceMode.VelocityChange);
            otherRb.AddTorque(otherRb.angularVelocity * -0.05f, ForceMode.VelocityChange);
        }

        if (collider.gameObject.CompareTag("Ball"))
        {
            collider.gameObject.transform.position = new Vector3(0, 4, 0);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.gameObject.transform.position = target;
        }
    }
}
