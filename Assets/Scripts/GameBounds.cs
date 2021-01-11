using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBounds : MonoBehaviour
{
    public Vector3 teleportTarget;

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.transform.position = teleportTarget;
    }
}
