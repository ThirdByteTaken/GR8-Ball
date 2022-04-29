using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Animator animator;
    Rigidbody rb_Ball; // It doens't like us naming it rigidbody
    const float BallSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb_Ball = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb_Ball.velocity = rb_Ball.velocity.normalized * BallSpeed;
    }

    void OnTriggerEnter(Collider collider)
    {
        print("hit");
        if (collider.gameObject.CompareTag("Hole"))
        {
            Destroy(rb_Ball);
            animator.SetTrigger("GameOver");
        }
    }
}
