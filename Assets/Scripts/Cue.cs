using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour
{
    Camera MainCamera;

    [SerializeField]
    Transform CueHolder, CueBall;

    Animator animator;
    Rigidbody rb_CueBall;

    [SerializeField]
    float Force;
    bool isTracking;

    // Start is called before the first frame update
    public void Start()
    {
        MainCamera = Camera.main;
        rb_CueBall = CueBall.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isTracking = true;
    }



    // Update is called once per frame
    void Update()
    {
        if (isTracking) UpdateCueLocation();
        if (Input.GetMouseButtonDown(0))
        {
            isTracking = false;
            animator.SetTrigger("Swing");
            Invoke("SwingCue", 5 / 6f);
        }
    }

    void UpdateCueLocation()
    {
        CueHolder.transform.position = CueBall.transform.position + new Vector3(0, 5, 0);
        //Vector2 direction = ((Vector2)MainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)CueHolder.transform.position);
        //CueHolder.transform.forward = direction;
        CueHolder.transform.LookAt(MainCamera.ScreenToWorldPoint(Input.mousePosition));
        var Rotation = CueHolder.transform.rotation;
        Rotation = Quaternion.Euler(-90, Rotation.eulerAngles.y, Rotation.eulerAngles.z);
        CueHolder.transform.rotation = Rotation;
    }
    void SwingCue()
    {
        rb_CueBall.AddForce(-CueHolder.transform.up * Force);
        gameObject.SetActive(false);
    }
}
