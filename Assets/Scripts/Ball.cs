using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Animator animator;
    Rigidbody rb_Ball; // It doesn't like us naming it rigidbody 
    AudioSource audioSource;

    SphereCollider col_Ball;

    const float BallSpeed = 10;

    [SerializeField]
    bool IsCueBall;
    [SerializeField]
    bool IsUncollidedCueBall; // Cue ball before hitting other balls - makes breaking look better

    public bool BallIsStriped;

    Vector3 FellHolePosition;

    [SerializeField]
    int BallNumber;

    public static int StripedBalls;
    public static int SolidBalls;
    public static bool IsPlayingStripes;

    // Start is called before the first frame update
    void Start()
    {
        StripedBalls = 7;
        SolidBalls = 7;
        animator = GetComponent<Animator>();
        rb_Ball = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        col_Ball = GetComponent<SphereCollider>();
        IsUncollidedCueBall = IsCueBall;
    }

    // Update is called once per frame
    void Update()
    {
        rb_Ball.velocity = rb_Ball.velocity.normalized * BallSpeed * ((IsUncollidedCueBall) ? 2 : 1);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Hole"))
        {
            print("Striped balls:" + StripedBalls);
            collider.GetComponent<AudioSource>().Play();
            if (StripedBalls == 7 && SolidBalls == 7)
            {
                if (IsCueBall && IsUncollidedCueBall) GameManager.Instance.LoseGame();
                else if (!IsCueBall)
                {
                    IsPlayingStripes = BallIsStriped;
                    if (IsPlayingStripes)
                    {
                        GameManager.Instance.ShowStripedIndicator();
                    }
                    else
                    {
                        GameManager.Instance.ShowSolidsIndicator();
                    }
                }
            }

            if (BallNumber != 8)
            {
                if (!IsCueBall)
                {
                    if (BallIsStriped) StripedBalls--;
                    else SolidBalls--;
                }
            }
            else
            {
                if ((IsPlayingStripes && StripedBalls <= 0) || (!IsPlayingStripes && SolidBalls <= 0))
                    GameManager.Instance.WinGame();
                else
                    GameManager.Instance.LoseGame();
            }

            rb_Ball.velocity = Vector3.zero;
            rb_Ball.constraints = RigidbodyConstraints.FreezePosition;
            rb_Ball.MoveRotation(Quaternion.Euler(rb_Ball.rotation.eulerAngles * 0.1f));
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            FellHolePosition = collider.transform.position;
            Invoke("FallIntoHole", 0.01f);
            animator.SetTrigger("GameOver");

        }
    }

    public void FallIntoHole()
    {
        transform.position = Vector3.Lerp(transform.position, FellHolePosition, .05f);
        GameManager.Instance.DisableBallIndicator(BallNumber);
        Invoke("FallIntoHole", .01f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            IsUncollidedCueBall = false;
            if (Random.Range(0, 1f) > 0.5) return;
            audioSource.pitch = Random.Range(0.5f, 0.8f);
            audioSource.volume = Random.Range(0.5f, 0.8f);
            audioSource.Play();
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            other.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
