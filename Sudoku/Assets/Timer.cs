using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;
    float time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        var minutes = time / 60;
        var seconds = time % 60;

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
