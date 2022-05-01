using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    GameObject WinMenu, LoseMenu, Everything, pref_Everything;

    void Start()
    { Instance = this; }

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
