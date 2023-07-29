using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimeTextManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float currentTime = 0f;
    private TMP_Text timerText;

    void Start()
    {
        // Get reference to the Text component for displaying the timer
        timerText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}
