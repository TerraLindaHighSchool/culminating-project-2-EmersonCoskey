using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followObject;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - followObject.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = followObject.transform.position + offset;
    }
}
