using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestTimes : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI firstPlace;
    [SerializeField] TextMeshProUGUI secondPlace;
    [SerializeField] TextMeshProUGUI thirdPlace;

    public List<float> timings = new List<float>(new float[3]);


    void Start()
    {
        firstPlace.text = $"1. {ConvertToMinutes(timings[0])}";
        secondPlace.text = $"2. {ConvertToMinutes(timings[1])}";
        thirdPlace.text = $"3. {ConvertToMinutes(timings[2])}";
    }

    string ConvertToMinutes(float time)
    {
        var minutes = time / 60;
        var seconds = time % 60;

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void RefreshStandings()
    {
        firstPlace.text = $"1. {ConvertToMinutes(timings[0])}";
        secondPlace.text = $"2. {ConvertToMinutes(timings[1])}";
        thirdPlace.text = $"3. {ConvertToMinutes(timings[2])}";
    }

    public void LoadStandings(float[] topThree)
    {
        for (int j = 0; j < 3; j++)
            timings[j] = topThree[j];

        RefreshStandings();
    }

    public void ClearStandings()
    {
        for (int j = 0; j < 3; j++)
            timings[j] = 0f;

        RefreshStandings();
    }

    public void SetPlaces(float timing)
    {
        if (timings[0] == 0f)
        {
            timings[0] = timing;
            RefreshStandings();
            return;
        }
        else if (timings[1] == 0f)
        {
            timings[1] = timing;
            if (timings[1] < timings[0])
            {
                float temp = timings[0];
                timings[0] = timing;
                timings[1] = temp;
            }
            RefreshStandings();
            return;
        }
        else if (timings[2] == 0f)
        {
            timings[2] = timing;
            timings.Sort();
            RefreshStandings();
        }
        else
        {
            timings.Add(timing);
            timings.Sort();
            RefreshStandings();
            timings.RemoveAt(3);
        }

    }

}
