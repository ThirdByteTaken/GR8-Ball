using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue : MonoBehaviour
{
    Camera MainCamera;

    [SerializeField]
    Transform CueHolder, CueBall;

    Rigidbody rb_CueBall;

    [SerializeField]
    float Force;
    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
        rb_CueBall = CueBall.GetComponent<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        CueHolder.transform.position = CueBall.transform.position + new Vector3(0, 5, 0);
        //Vector2 direction = ((Vector2)MainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)CueHolder.transform.position);
        //CueHolder.transform.forward = direction;
        CueHolder.transform.LookAt(MainCamera.ScreenToWorldPoint(Input.mousePosition));
        var Rotation = CueHolder.transform.rotation;
        CueHolder.transform.rotation = Rotation;
        if (Input.GetMouseButtonDown(0))
        {
            rb_CueBall.AddForce(CueHolder.transform.forward * Force);
        }
    }
}
