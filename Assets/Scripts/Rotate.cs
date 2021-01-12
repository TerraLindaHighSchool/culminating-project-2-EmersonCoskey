using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateRate;

    private void Update()
    {
        transform.Rotate(new Vector3(rotateRate, rotateRate, rotateRate));
    }
}
