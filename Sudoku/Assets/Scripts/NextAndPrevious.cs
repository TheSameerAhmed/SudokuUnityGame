using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;

public class NextAndPrevious : MonoBehaviour
{
    [SerializeField] CellScript1 cells;
    [SerializeField] Timer timer;
    [SerializeField] PuzzleManager puzzleManager;
    [SerializeField] TextMeshProUGUI puzzleCountText;
    [SerializeField] GameObject nextPuzzleButton;
    [SerializeField] GameObject previousPuzzleButton;

    public int index { get; set; } = 0;
    
    
    readonly List<int[,]> AllPuzzles = new List<int[,]>();
    //int[,] model = new int[9, 9] { {5,3,0,0,7,0,0,0,0},
    //                               {6,0,0,1,9,5,0,0,0},
    //                               {0,9,8,0,0,0,0,6,0},
    //                               {8,0,0,0,6,0,0,0,3},
    //                               {4,0,0,8,0,3,0,0,1},
    //                               {7,0,0,0,2,0,0,0,6},
    //                               {0,6,0,0,0,0,2,8,0},
    //                               {0,0,0,4,1,9,0,0,5},
    //                               {0,0,0,0,8,0,0,7,9},};

    //int[,] model1 = new int[9, 9] { {3,0,0,8,0,1,0,0,2},
    //                               {2,0,1,0,3,0,6,0,4},
    //                               {0,0,0,2,0,4,0,0,0},
    //                               {8,0,9,0,0,0,1,0,6},
    //                               {0,6,0,0,0,0,0,5,0},
    //                               {7,0,2,0,0,0,4,0,9},
    //                               {0,0,0,5,0,9,0,0,0},
    //                               {9,0,4,0,8,0,7,0,5},
    //                               {6,0,0,1,0,7,0,0,3},};

    int[,] model = new int[9, 9] { {5,3,4,6,7,8,9,1,2},
                                   {6,7,2,1,9,5,3,4,8},
                                   {1,9,8,3,4,2,5,6,7},
                                   {8,5,9,7,6,1,4,2,3},
                                   {4,2,6,8,5,3,7,9,1},
                                   {7,1,3,9,2,4,8,5,6},
                                   {9,6,1,5,3,7,2,8,4},
                                   {2,8,7,4,1,9,6,0,5},
                                   {3,4,5,2,8,6,0,7,9},};

    int[,] model1 = new int[9, 9] { {3,4,6,8,9,1,5,7,2},
                                   {2,9,1,7,3,5,6,8,4},
                                   {5,7,8,2,6,4,3,9,1},
                                   {8,5,9,4,7,3,1,2,6},
                                   {4,6,3,9,1,2,8,5,7},
                                   {7,1,2,6,5,8,4,3,9},
                                   {1,3,7,5,4,9,2,6,8},
                                   {9,2,4,0,8,0,7,1,5},
                                   {6,8,5,1,2,7,0,0,3},};


    List<bool> modifiedPuzzlesMap = new List<bool>();
    List<int[,]> savedStatesOfModifiedPuzzles = new List<int[,]>();
    List<HashSet<Tuple<int, int>>> savedFixedValues = new List<HashSet<Tuple<int, int>>>();
    public List<bool> savedValidationState = new List<bool>();

    void Awake()
    {
        AllPuzzles.Add(model);
        AllPuzzles.Add(model1);
        modifiedPuzzlesMap.Add(false);
        modifiedPuzzlesMap.Add(false);
        savedValidationState.Add(false);
        savedValidationState.Add(false);
        savedStatesOfModifiedPuzzles.Add(new int[9, 9]);
        savedStatesOfModifiedPuzzles.Add(new int[9, 9]);
        savedFixedValues.Add(new HashSet<Tuple<int, int>>());
        savedFixedValues.Add(new HashSet<Tuple<int, int>>());
        cells.LoadPuzzle(AllPuzzles[index]);
        UpdatePuzzleCount();
    }
    void UpdatePuzzleCount()
    {
        if (index == 0)
        {
            previousPuzzleButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
            previousPuzzleButton.GetComponent<Button>().enabled = false;
            if (AllPuzzles.Count > 1)
            {
                nextPuzzleButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
                nextPuzzleButton.GetComponent<Button>().enabled = true;
            }
        }
        else if(index == AllPuzzles.Count - 1)
        {
            previousPuzzleButton.GetComponent<Button>().enabled = true;
            previousPuzzleButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
            nextPuzzleButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
            nextPuzzleButton.GetComponent<Button>().enabled = false;
        }
        puzzleCountText.text = $"{index + 1} / {AllPuzzles.Count}";
    }

    public void NextPuzzle()
    {
        
        savedStatesOfModifiedPuzzles[index] = puzzleManager.numbersInPuzzle;
        modifiedPuzzlesMap[index] = true;

        savedFixedValues[index] = cells.fixedValues;
        cells.WipePuzzleForNew();
        index++;
        if (modifiedPuzzlesMap[index] == false)
        {
            puzzleManager.numbersInPuzzle = new int[9, 9];
            cells.LoadPuzzle(AllPuzzles[index]);

        }
        else if(savedValidationState[index])
        {
            cells.LoadSavedPuzzle(savedStatesOfModifiedPuzzles[index], savedFixedValues[index], AllPuzzles[index]);
            cells.LoadCompletedPuzzle();
            puzzleManager.numbersInPuzzle = savedStatesOfModifiedPuzzles[index];
        }
        else
        {
            puzzleManager.numbersInPuzzle = savedStatesOfModifiedPuzzles[index];
            cells.LoadSavedPuzzle(savedStatesOfModifiedPuzzles[index], savedFixedValues[index], AllPuzzles[index]);
        }
        timer.RestartTimer();
        UpdatePuzzleCount();
    }


    public void PreviousPuzzle()
    {
        savedStatesOfModifiedPuzzles[index] = puzzleManager.numbersInPuzzle;
        modifiedPuzzlesMap[index] = true;
        //if (puzzleManager.isValidate())
        //{
        //    savedValidationState[index] = true;
        //}
        savedFixedValues[index] = cells.fixedValues;
        index--;
        cells.WipePuzzleForNew();
        Debug.Log($"State of previous puzzle: {savedValidationState[index]}");
        if (modifiedPuzzlesMap[index] == false)
        {
            puzzleManager.numbersInPuzzle = new int[9, 9];
            cells.LoadPuzzle(AllPuzzles[index]);
        }
        else if(savedValidationState[index])
        {
            cells.LoadSavedPuzzle(savedStatesOfModifiedPuzzles[index], savedFixedValues[index], AllPuzzles[index]);
            cells.LoadCompletedPuzzle();
            puzzleManager.numbersInPuzzle = savedStatesOfModifiedPuzzles[index];
        }
        else
        {
            puzzleManager.numbersInPuzzle = savedStatesOfModifiedPuzzles[index];
            cells.LoadSavedPuzzle(savedStatesOfModifiedPuzzles[index], savedFixedValues[index], AllPuzzles[index]);
        }
        timer.RestartTimer();
        UpdatePuzzleCount();
    }

    public void ResetButton()
    {
        modifiedPuzzlesMap[index] = false;
        savedValidationState[index] = false;
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
