using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    GameObject WinMenu, LoseMenu, Everything, pref_Everything, BallsParent;

    void Start()
    {
        Instance = this;
        RandomizeBallPositions();
    }

    public void RandomizeBallPositions()
    {
        List<Vector3> ballPositions = new List<Vector3>();
        List<Transform> tf_unplacedBalls = new List<Transform>();
        for (int i = 1; i < BallsParent.transform.childCount; i++) // index starts at 1 to skip cue ball
        {
            var ball = BallsParent.transform.GetChild(i);
            ballPositions.Add(ball.position);
            tf_unplacedBalls.Add(ball);
        }
        List<int> centerIndices = new List<int> { 4, 7, 8 };
        int index = centerIndices[Random.Range(0, centerIndices.Count)];
        tf_unplacedBalls[7].position = ballPositions[index];
        tf_unplacedBalls.RemoveAt(7);
        ballPositions.RemoveAt(index);
        foreach (Transform ball in tf_unplacedBalls)
        {
            var newPosition = ballPositions[Random.Range(0, ballPositions.Count)];
            ball.position = newPosition;
            ballPositions.Remove(newPosition);
        }
    }
    public void WinGame()
    { WinMenu.SetActive(true); }

    public void LoseGame()
    { LoseMenu.SetActive(true); }

    public void ResetGame()
    {
        Destroy(Everything);
        Everything = Instantiate(pref_Everything);
        Ball.StripedBalls = 7;
        Ball.SolidBalls = 7;
    }

}
