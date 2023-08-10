using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{

    // Start is called before the first frame update
    private float totalTime = 4f;
    private float timeRemaining;
    private bool timerIsRunning = false;
    private TMP_Text countdownText;

    public float TimeRemaining { get { return timeRemaining; } set { timeRemaining = value; } }

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to the Text component for displaying the timer
        countdownText = GetComponent<TMP_Text>();
        timerIsRunning = true;
        timeRemaining = totalTime;
    }

    void FixedUpdate()
    {
        if (timerIsRunning)
        {
            if (timeRemaining >= 1)
            {
                int seconds = Mathf.FloorToInt(timeRemaining % 60f);
                timeRemaining -= Time.fixedDeltaTime;
                countdownText.text = string.Format("{0:0}", seconds);
            }
            else if (timeRemaining > 0)
            {
                timeRemaining -= Time.fixedDeltaTime;
                countdownText.text = string.Format("GO!");
            }
            else
            {
                Debug.Log("Game start");
                timerIsRunning = false;
                countdownText.text = string.Format("");
            }
        }
    }
}
