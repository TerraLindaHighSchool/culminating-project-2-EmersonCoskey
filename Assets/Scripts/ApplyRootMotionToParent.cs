using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyRootMotionToParent : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();    
    }

    private void OnAnimatorMove()
    {
        transform.parent.transform.position += anim.deltaPosition;
    }
}
