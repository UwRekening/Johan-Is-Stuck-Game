using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public bool stopWatch = false;
    float currentTime;
    public int startMinutes;
    public TextMeshProUGUI text;

    private void Start()
    {
        stopWatch = true;
    }

    void Update()
    {
        //Checkt als stopWatch true is
        if (stopWatch)
        {
            //Verander de Time naar currentTime - Time.deltaTime
            currentTime = currentTime - Time.deltaTime;

            //Zorgt ervoor dat er secondes zijn.
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);

            //Laat het zien in de UI text
            text.text = timeSpan.ToString(@"mm\:ss\:fff");
        }
    }
}
