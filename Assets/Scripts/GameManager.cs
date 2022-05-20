using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    GameObject WinMenu, LoseMenu, Everything, pref_Everything, BallsParent, Solid, Striped;

    [SerializeField]
    GameObject[] BallIndictators;

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
        Solid.SetActive(false);
        Striped.SetActive(false);
        BallIndictators.ToList().ForEach(x => x.SetActive(true));
        for (int i = 0; i < BallIndictators.Length; i++)
        {
            //BallIndictators[i].transform.position = new Vector3(BallIndictators[i].transform.position.x, BallIndictators[i].transform.position.y, 6.4f - (0.8f * i));
        }
    }

    public void ShowSolidsIndicator()
    {
        Solid.SetActive(true);
    }

    public void ShowStripedIndicator()
    {
        Striped.SetActive(true);
    }

    public void DisableBallIndicator(int BallNumber)
    {
        BallIndictators[BallNumber - 1].SetActive(false);
        var ActiveBalls/*:P*/ = BallIndictators.Where(x => x.activeInHierarchy && (x.CompareTag("StripedBall") == Ball.IsPlayingStripes)).ToArray();
        for (int i = 0; i < ActiveBalls.Length; i++)
        {
            ActiveBalls[i].transform.localPosition = new Vector3(ActiveBalls[i].transform.localPosition.x, ActiveBalls[i].transform.localPosition.y, 2f - (0.8f * i));
        }

    }

}
