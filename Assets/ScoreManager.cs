using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText; // Reference to the UI Text component for displaying the score
    public string team1 = "Red";
    public string team2 = "Blue";
    private int team1Score = 0; // The current score
    private int team2Score = 0; // The current score




    void Start()
    {
        UpdateScoreText();
        //scoreText = GetComponent<TMP_Text>();

    }

    public void AddScore(int value, int team)
    {
        switch (team)
        {
            case 1:
                team1Score += value;
                break;
            case 2:
                team2Score += value;
                break;
        }
        UpdateScoreText();
    }

    public void SetTeamNames(string team1Name, string team2Name)
    {
        team1 = team1Name;
        team2 = team2Name;
    }
    void UpdateScoreText()
    {
        string scoreString = team1 + ": " + team1Score.ToString() + "\n" + team2 + ": " + team2Score.ToString();
        scoreText.text = scoreString;
    }
}