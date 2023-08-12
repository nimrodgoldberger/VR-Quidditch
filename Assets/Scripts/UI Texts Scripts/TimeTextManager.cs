using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeTextManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float totalTime = 120.0f;
    private float timeRemaining;
    private bool timerIsRunning = false;
    private TMP_Text timerText;

    public float TimeRemaining { get { return timeRemaining; } set { timeRemaining = value; } }

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
            }

            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        }
    }

}
