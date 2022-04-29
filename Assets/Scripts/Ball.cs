using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Animator animator;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        print("hit");
        if (collider.gameObject.CompareTag("Hole"))
        {
            Destroy(rigidbody);
            animator.SetTrigger("GameOver");
        }
    }
}
