using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class NextAndPrevious : MonoBehaviour
{
    [SerializeField] CellScript1 cells;
    [SerializeField] Timer timer;
    [SerializeField] PuzzleManager puzzleManager;
    [SerializeField] TextMeshProUGUI puzzleCountText;

    int index = 0;
    
    readonly List<int[,]> AllPuzzles = new List<int[,]>();
    int[,] model = new int[9, 9] { {5,3,0,0,7,0,0,0,0},
                                   {6,0,0,1,9,5,0,0,0},
                                   {0,9,8,0,0,0,0,6,0},
                                   {8,0,0,0,6,0,0,0,3},
                                   {4,0,0,8,0,3,0,0,1},
                                   {7,0,0,0,2,0,0,0,6},
                                   {0,6,0,0,0,0,2,8,0},
                                   {0,0,0,4,1,9,0,0,5},
                                   {0,0,0,0,8,0,0,7,9},};

    int[,] model1 = new int[9, 9] { {3,0,0,8,0,1,0,0,2},
                                   {2,0,1,0,3,0,6,0,4},
                                   {0,0,0,2,0,4,0,0,0},
                                   {8,0,9,0,0,0,1,0,6},
                                   {0,6,0,0,0,0,0,5,0},
                                   {7,0,2,0,0,0,4,0,9},
                                   {0,0,0,5,0,9,0,0,0},
                                   {9,0,4,0,8,0,7,0,5},
                                   {6,0,0,1,0,7,0,0,3},};

    List<bool> modifiedPuzzlesMap = new List<bool>();
    List<int[,]> savedStatesOfModifiedPuzzles = new List<int[,]>();
    List<HashSet<Tuple<int, int>>> savedFixedValues = new List<HashSet<Tuple<int, int>>>();
    List<int[,]> savedDerivedSolutions = new List<int[,]>();

    void Awake()
    {
        AllPuzzles.Add(model);
        AllPuzzles.Add(model1);
        modifiedPuzzlesMap.Add(false);
        modifiedPuzzlesMap.Add(false);
        //savedStatesOfModifiedPuzzles.Capacity = 2;
        //savedFixedValues.Capacity = 2;
        savedStatesOfModifiedPuzzles.Add(new int[9, 9]);
        savedStatesOfModifiedPuzzles.Add(new int[9, 9]);
        savedFixedValues.Add(new HashSet<Tuple<int, int>>());
        savedFixedValues.Add(new HashSet<Tuple<int, int>>());
        cells.LoadPuzzle(AllPuzzles[index]);
        UpdatePuzzleCount();
    }
    void UpdatePuzzleCount()
    {
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
        savedFixedValues[index] = cells.fixedValues;
        index--;
        cells.WipePuzzleForNew();
        if (modifiedPuzzlesMap[index] == false)
        {
            puzzleManager.numbersInPuzzle = new int[9, 9];
            cells.LoadPuzzle(AllPuzzles[index]);
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
        }

        for(int j = 0; j < AllPuzzles.Count; j++)
        {
            savedStatesOfModifiedPuzzles[j] = (int[,])AllPuzzles[j].Clone();
        }

        cells.WipePuzzleForNew();
        puzzleManager.numbersInPuzzle = new int[9, 9];

        cells.LoadPuzzle(AllPuzzles[index]);
        timer.RestartTimer();
    }
}
