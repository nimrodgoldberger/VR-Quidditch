using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class SceneInitializer : MonoBehaviour
{
    public GameObject initialScreen;
    public GameObject gameOverScreen;

    private PlayerTeam[] teams = new PlayerTeam[2];
    private int[] scores = new int[2];
    public int myTeamIndex = 0; //TODO: Set right value after multiplayer - before multi the player's team is always team1. Might also need to be passed from previous scene

    [SerializeField] TMP_Text resultText;
    [SerializeField] TMP_Text scoreText;

    public string sceneNameToLoad = "MenuScene";

    void Start()
    {
        if (PlayerPrefs.HasKey("IsOver"))
        {
            // Load the saved teams and scores data from PlayerPrefs
            teams[0] = (PlayerTeam)PlayerPrefs.GetInt("Team1");
            teams[1] = (PlayerTeam)PlayerPrefs.GetInt("Team2");
            scores[0] = PlayerPrefs.GetInt("Team1Score");
            scores[1] = PlayerPrefs.GetInt("Team2Score");

            gameOverScreen.SetActive(true);
            initialScreen.SetActive(false);
            DisplayResultMessage();
            PlayerPrefs.DeleteKey("IsOver");
        }
        else
        {
            initialScreen.SetActive(true);
            gameOverScreen.SetActive(false);
        }
    }

    private void DisplayResultMessage()
    {
        if (scores[myTeamIndex] > scores[Math.Abs(myTeamIndex - 1)]) // abs(0-1)=1 and abs(1-1)=0 --> gives the other teams index
            resultText.text = "Champions!";
        else if (scores[myTeamIndex] < scores[Math.Abs(myTeamIndex - 1)])
            resultText.text = "Better luck next time...";
        else
            resultText.text = "It's a Tie!";
        scoreText.text = scores[myTeamIndex] + " : " + scores[Math.Abs(myTeamIndex - 1)];
    }
}
