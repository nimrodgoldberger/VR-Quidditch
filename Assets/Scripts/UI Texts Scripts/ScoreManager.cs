using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText; // Reference to the UI Text component for displaying the score
    private PlayerTeam team1;
    private PlayerTeam team2;
    private int team1Score; // The current score
    private int team2Score; // The current score

    public string[] teamNames; // Array of team names (size: 4)

    void Start()
    {
        // Load the saved teams data from PlayerPrefs
        team1 = (PlayerTeam)PlayerPrefs.GetInt("Team1");
        team2 = (PlayerTeam)PlayerPrefs.GetInt("Team2");

        team1Score = 0;
        team2Score = 0;

        // Set score text at the beggining
        SetTeamScore(team1, 0);

    }

    //public void AddScore(int value, int team)
    //{
    //    switch (team)
    //    {
    //        case 1:
    //            team1Score += value;
    //            break;
    //        case 2:
    //            team2Score += value;
    //            break;
    //    }
    //    UpdateScoreText();
    //}

    //void UpdateScoreText()
    //{
    //    string scoreString = team1 + ": " + team1Score.ToString() + "\n" + team2 + ": " + team2Score.ToString();
    //    scoreText.text = scoreString;
    //}

    public void SetTeamScore(PlayerTeam team, int additionalPoints)
    {

        if (team == team1)
        {
            team2Score = GetScoreForTeam(team2);
            team1Score = GetScoreForTeam(team1) + additionalPoints;
        }
        else if (team == team2)
        {
            team2Score = GetScoreForTeam(team2) + additionalPoints;
            team1Score = GetScoreForTeam(team1);
        }
        else
        {
            //Debug.Log("non existing team score requested to be set");
            team2Score = -1;
            team1Score = -1;
        }

        // Set the first team's name in the score text
        scoreText.text = teamNames[(int)team1];
        scoreText.text += ":" + team1Score;

        // Add a newline to separate the two teams
        scoreText.text += Environment.NewLine;

        // Set the second team's name in the score text
        scoreText.text += teamNames[(int)team2];
        scoreText.text += ":" + team2Score;
    }
    public int GetScoreForTeam(PlayerTeam team)
    {

        if (team == team1)
            return team1Score;
        else if (team == team2)
            return team2Score;
        else
        {
            //Debug.Log("non existing team score requested");
            return -1;
        }
    }

    public PlayerTeam GetTeam1()
    {
        return team1;
    }

    public PlayerTeam GetTeam2()
    {
        return team2;
    }

    public PlayerTeam GetWinner()
    {
        if (team1Score > team2Score)
            return team1;
        else if (team2Score > team1Score)
            return team2;
        else
            return PlayerTeam.None; //EQUALITY
    }
}