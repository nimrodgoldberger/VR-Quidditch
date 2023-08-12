using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScreenResultDisplay : MonoBehaviour
{
    private PlayerTeam [] teams = new PlayerTeam[2];
    private int [] scores = new int[2];

    private int team1Score;
    private int team2Score;
    public int myTeamIndex = 0; //TODO: Set right value after multiplayer - before multi the player's team is always team1. Might also need to be passed from previous scene

    [SerializeField] TMP_Text resultText;
    [SerializeField] TMP_Text scoreText;

    public string sceneNameToLoad = "MenuScene";
    public GameObject specificScreenIdentifier;
    public GameObject turnoffScreenIdentifier;

    // Start is called before the first frame update
    void Start()
    {
        // Load the saved teams and scores data from PlayerPrefs
        teams[0] = (PlayerTeam)PlayerPrefs.GetInt("Team1");
        teams[1] = (PlayerTeam)PlayerPrefs.GetInt("Team2");
        scores[0] = PlayerPrefs.GetInt("Team1Score");
        scores[1] = PlayerPrefs.GetInt("Team2Score");
    }

    public void DisplayResultMessage()
    {
        if (scores[myTeamIndex] >= scores[Math.Abs(myTeamIndex-1)]) // abs(0-1)=1 and abs(1-1)=0 --> gives the other teams index
            resultText.text = "Champions!";
        else
            resultText.text = "Better luck next time...";
        
        scoreText.text = scores[myTeamIndex] + " : " + scores[Math.Abs(myTeamIndex - 1)];
    }

    public void EndGame()
    {
        SceneManager.LoadScene(sceneNameToLoad);

        if (specificScreenIdentifier != null)
        {
            specificScreenIdentifier.SetActive(true);
            specificScreenIdentifier.SetActive(false);
        }
        DisplayResultMessage();
    }
}
