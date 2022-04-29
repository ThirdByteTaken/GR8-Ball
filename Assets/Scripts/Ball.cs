using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Animator animator;
    Rigidbody rb_Ball; // It doens't like us naming it rigidbody
    AudioSource audioSource;
    const float BallSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb_Ball = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            collider.GetComponent<AudioSource>().Play();
            Destroy(rb_Ball);
            animator.SetTrigger("GameOver");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball") && Random.Range(0, 1f) > 0.5) return;
        audioSource.pitch = Random.Range(0.5f, 0.8f);
        audioSource.volume = Random.Range(0.5f, 0.8f);
        audioSource.Play();
    }
}
