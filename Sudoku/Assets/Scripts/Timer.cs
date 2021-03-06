﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;
    public float time;

    void Start()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        var minutes = time / 60;
        var seconds = time % 60;

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RestartTimer()
    {
        time = 0;
        enabled = true;
    }

    public void StopTimer()
    {
        enabled = false;
    }

    public void TurnOnTimer()
    {
        enabled = true;
    }

    public void DisplaySavedTime(float savedTime)
    {
        enabled = false;

        var minutes = savedTime / 60;
        var seconds = savedTime % 60;
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }
}
