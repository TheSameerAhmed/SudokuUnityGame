﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;
using System.IO;

public class NextAndPrevious : MonoBehaviour
{
    [SerializeField] CellScript1 cells;
    [SerializeField] Timer timer;
    [SerializeField] PuzzleManager puzzleManager;
    [SerializeField] TextMeshProUGUI puzzleCountText;
    [SerializeField] TextMeshProUGUI difficultyText;
    [SerializeField] GameObject nextPuzzleButton;
    [SerializeField] GameObject previousPuzzleButton;
    [SerializeField] BestTimes bestTimes;

    public int difficulty { get; set; } // 1 is for easy, 2 for medium, 3 for hard
 
    public int index { get; set; } = 0;
        
    List<int[,]> AllPuzzles = new List<int[,]>();      

    List<bool> modifiedPuzzlesMap = new List<bool>();
    List<int[,]> savedStatesOfModifiedPuzzles = new List<int[,]>();
    List<HashSet<Tuple<int, int>>> savedFixedValues = new List<HashSet<Tuple<int, int>>>();
    List<float> latestSavedTimes = new List<float>();
    List<float[]> bestTimesSaved = new List<float[]>();

    public List<float> latestSavedTimesOfCompletedPuzzles = new List<float>();
    public List<bool> savedValidationState = new List<bool>();


    public void DisplayPuzzle()
    {
        if (difficulty == 1)
        {
            AllPuzzles = DownloadSudokuPuzzles.ParsePuzzles("Easy.txt");
            difficultyText.text = "Easy";
        }
        else if (difficulty == 2)
        {
            AllPuzzles = DownloadSudokuPuzzles.ParsePuzzles("Medium.txt");
            difficultyText.text = "Medium";
        }
        else if (difficulty == 3)
        {
            AllPuzzles = DownloadSudokuPuzzles.ParsePuzzles("Hard.txt");
            difficultyText.text = "Hard";
        }

        InitializeLists();
        cells.LoadPuzzle(AllPuzzles[index]);
        UpdatePuzzleCount();
    }


    void InitializeLists()
    {
        for(int i = 0; i < AllPuzzles.Count; i++)
        {
            modifiedPuzzlesMap.Add(false);
            savedValidationState.Add(false);
            savedStatesOfModifiedPuzzles.Add(new int[9, 9]);
            savedFixedValues.Add(new HashSet<Tuple<int, int>>());
            latestSavedTimes.Add(0f);
            latestSavedTimesOfCompletedPuzzles.Add(0f);
            bestTimesSaved.Add(new float[3]);
        }
    }
    void UpdatePuzzleCount()
    {
        if (index == 0)
        {
            previousPuzzleButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
            previousPuzzleButton.GetComponent<Button>().enabled = false;
            if (AllPuzzles.Count > 1)
            {
                nextPuzzleButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0f,0.08235294f, 0.2352941f,1f);
                nextPuzzleButton.GetComponent<Button>().enabled = true;
            }
        }
        else if(index == AllPuzzles.Count - 1)
        {
            previousPuzzleButton.GetComponent<Button>().enabled = true;
            previousPuzzleButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0f, 0.08235294f, 0.2352941f, 1f);
            nextPuzzleButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
            nextPuzzleButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            previousPuzzleButton.GetComponent<Button>().enabled = true;
            previousPuzzleButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0f, 0.08235294f, 0.2352941f, 1f);
            nextPuzzleButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0f, 0.08235294f, 0.2352941f, 1f);
            nextPuzzleButton.GetComponent<Button>().enabled = true;
        }
        puzzleCountText.text = $"{index + 1}/{AllPuzzles.Count}";
        Debug.Log(Path.GetFullPath("Resources"));
    }

    public void NextPuzzle()
    {
        
        savedStatesOfModifiedPuzzles[index] = puzzleManager.numbersInPuzzle;
        modifiedPuzzlesMap[index] = true;
        latestSavedTimes[index] = timer.time;

        for (int j = 0; j < 3; j++)
            bestTimesSaved[index][j] = bestTimes.timings[j];

        savedFixedValues[index] = cells.fixedValues;
        cells.WipePuzzleForNew();
        index++;
        if (modifiedPuzzlesMap[index] == false)
        {
            puzzleManager.numbersInPuzzle = new int[9, 9];
            cells.LoadPuzzle(AllPuzzles[index]);
            timer.RestartTimer();
            bestTimes.ClearStandings();
            puzzleManager.isValidated = false;
            UpdatePuzzleCount();

        }
        else if(savedValidationState[index])
        {
            cells.LoadSavedPuzzle(savedStatesOfModifiedPuzzles[index], savedFixedValues[index], AllPuzzles[index]);
            cells.LoadCompletedPuzzle();
            bestTimes.LoadStandings(bestTimesSaved[index]);
            puzzleManager.numbersInPuzzle = savedStatesOfModifiedPuzzles[index];
            UpdatePuzzleCount();
            timer.DisplaySavedTime(latestSavedTimesOfCompletedPuzzles[index]);
            puzzleManager.isValidated = true;
            
            return;
        }
        else
        {
            puzzleManager.numbersInPuzzle = savedStatesOfModifiedPuzzles[index];
            cells.LoadSavedPuzzle(savedStatesOfModifiedPuzzles[index], savedFixedValues[index], AllPuzzles[index]);
            timer.time = latestSavedTimes[index];
            bestTimes.LoadStandings(bestTimesSaved[index]);
            timer.TurnOnTimer();
            UpdatePuzzleCount();
            puzzleManager.isValidated = false;
        }

    }


    public void PreviousPuzzle()
    {
        savedStatesOfModifiedPuzzles[index] = puzzleManager.numbersInPuzzle;
        modifiedPuzzlesMap[index] = true;
        latestSavedTimes[index] = timer.time;

        for (int j = 0; j < 3; j++)
            bestTimesSaved[index][j] = bestTimes.timings[j];

        savedFixedValues[index] = cells.fixedValues;
        index--;
        cells.WipePuzzleForNew();
        Debug.Log($"State of previous puzzle: {savedValidationState[index]}");
        if (modifiedPuzzlesMap[index] == false)
        {
            puzzleManager.numbersInPuzzle = new int[9, 9];
            cells.LoadPuzzle(AllPuzzles[index]);
            timer.RestartTimer();
            bestTimes.ClearStandings();
            puzzleManager.isValidated = false;
            UpdatePuzzleCount();
        }
        else if(savedValidationState[index])
        {
            cells.LoadSavedPuzzle(savedStatesOfModifiedPuzzles[index], savedFixedValues[index], AllPuzzles[index]);
            cells.LoadCompletedPuzzle();
            puzzleManager.numbersInPuzzle = savedStatesOfModifiedPuzzles[index];
            UpdatePuzzleCount();
            timer.DisplaySavedTime(latestSavedTimesOfCompletedPuzzles[index]);
            bestTimes.LoadStandings(bestTimesSaved[index]);
            puzzleManager.isValidated = true;

            Debug.Log($"{latestSavedTimesOfCompletedPuzzles[index]} time inside the list inside PreviousPuzzle");
            return;
        }
        else
        {
            puzzleManager.numbersInPuzzle = savedStatesOfModifiedPuzzles[index];
            cells.LoadSavedPuzzle(savedStatesOfModifiedPuzzles[index], savedFixedValues[index], AllPuzzles[index]);
            timer.time = latestSavedTimes[index];
            timer.TurnOnTimer();
            bestTimes.LoadStandings(bestTimesSaved[index]);
            UpdatePuzzleCount();
            puzzleManager.isValidated = false;
        }

    }

    public void ResetButton()
    {
        modifiedPuzzlesMap[index] = false;
        savedValidationState[index] = false;
        latestSavedTimesOfCompletedPuzzles[index] = 0f;
        latestSavedTimes[index] = 0f;
        cells.WipePuzzleForNew();
        puzzleManager.numbersInPuzzle = new int[9, 9];
        savedStatesOfModifiedPuzzles[index] = (int[,]) AllPuzzles[index].Clone();
        Debug.Log($"{cells.fixedValues.Count} COUNT1");
        int n = 0;
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                if (AllPuzzles[index][i,j] == 0)
                    n++;
            }
        }
        Debug.Log($"{n} COUNT of 0s");
        cells.LoadPuzzle(AllPuzzles[index]);          
        Debug.Log($"{cells.fixedValues.Count} COUNT2");
        timer.RestartTimer();
    }

    public void ResetAllButton()
    {
        index = 0;

        for(int j = 0; j < AllPuzzles.Count; j++)
        {
            modifiedPuzzlesMap[j] = false;
            savedValidationState[j] = false;
            latestSavedTimesOfCompletedPuzzles[index] = 0f;
            latestSavedTimes[index] = 0f;
        }

        for(int j = 0; j < AllPuzzles.Count; j++)
        {
            savedStatesOfModifiedPuzzles[j] = (int[,])AllPuzzles[j].Clone();
        }

        cells.WipePuzzleForNew();
        puzzleManager.numbersInPuzzle = new int[9, 9];

        cells.LoadPuzzle(AllPuzzles[index]);
        timer.RestartTimer();
        UpdatePuzzleCount();
    }
}
