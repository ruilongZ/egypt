using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorController : MonoBehaviour
{
    float dirx;
    float diry;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirx = GetComponentInParent<PlayerMovementNew>().dir.x;
        diry = GetComponentInParent<PlayerMovementNew>().dir.y;
        animator.SetFloat("x", dirx * 100);
        animator.SetFloat("y", diry * 100);
    }
}
