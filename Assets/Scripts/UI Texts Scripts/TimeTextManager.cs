using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTextManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float totalTime = 124.0f; //adding 4 seconds for the countdown
    private float timeRemaining;
    private bool timerIsRunning = false;
    private TMP_Text timerText;

    public float TimeRemaining { get { return timeRemaining; } set { timeRemaining = value; } }

    [SerializeField] TeamPlayersManager teamManager;
    [SerializeField] GameObject scoreManager;

    void Start()
    {
        // Get reference to the Text component for displaying the timer
        timerText = GetComponent<TMP_Text>();
        timerIsRunning = true;
        timeRemaining = totalTime;
    }


    void Update()
    {
        if(timerIsRunning)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

            }
            else
            {
                //Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                PlayerTeam winningTeam = scoreManager.GetComponent<ScoreManager>().GetWinner();
                teamManager.SetBackAllPlayersToIdleState(winningTeam);
                //StartCoroutine(teamManager.GoalAnimations(winningTeam));
                int scoreTeam1 = scoreManager.GetComponent<ScoreManager>().GetScoreForTeam(teamManager.GetTeam1());
                int scoreTeam2 = scoreManager.GetComponent<ScoreManager>().GetScoreForTeam(teamManager.GetTeam2());
                teamManager.GameOver(scoreTeam1, scoreTeam2);
            }

            if(timeRemaining <= 120)
            {
                int minutes = Mathf.FloorToInt(timeRemaining / 60f);
                int seconds = Mathf.FloorToInt(timeRemaining % 60f);
                timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
            }
        }
    }
}
