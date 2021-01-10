using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public Vector3 target;
    public GameController gameController;
    
    private void OnTriggerStay(Collision collision)
    {
        Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
        if (collision.gameObject.CompareTag("Enemy"))
        {
            otherRb.AddForce(otherRb.velocity * -0.2f, ForceMode.VelocityChange);
            otherRb.AddTorque(otherRb.angularVelocity * -0.2f, ForceMode.VelocityChange);
        }

        if (collision.gameObject.CompareTag("ball"))
        {
            collision.gameObject.transform.position = new Vector3(0, 4, 0);
        }
    }

    private void OnTriggerExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.transform.position = target;
        }
    }
}
