using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] Timer timer;
    public int[,] numbersInPuzzle = new int[9,9];

    public int[,] derivedSolution = new int[9, 9];

    public void InsertValue(string index, int value)
    {
        //Debug.Log("Entered Insert");
        int row = Convert.ToInt32(index.Substring(0, 1));
        int column = Convert.ToInt32(index.Substring(1, 1));

        numbersInPuzzle[row,column] = value;

        PrintValue();
    }

    public void DeleteValue(string index)
    {
        int row = Convert.ToInt32(index.Substring(0, 1));
        int column = Convert.ToInt32(index.Substring(1, 1));

        numbersInPuzzle[row,column] = 0;

        PrintValue();
    }

    void PrintValue()
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sb1 = new StringBuilder();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                sb.Append(derivedSolution[i, j]);
                sb1.Append(numbersInPuzzle[i, j]);
            }
            Debug.Log($"derived soution {sb} space numbers in puzzle {sb1}");
            sb = new StringBuilder();
            sb1 = new StringBuilder();
            
        }

    }

    public bool SolvePuzzle(int[,] findSolution)
    {
        derivedSolution = findSolution;
        Tuple<int, int> EmptyBox = FindEmptySpace();

        if (EmptyBox == null)
        {
            return true;
        }
        //Debug.Log($"Empty Space: {EmptyBox.Item1} and {EmptyBox.Item2}");
        for (int i = 1; i < 10; i++)
        {
            if (IsValid(i, EmptyBox))
            {
                derivedSolution[EmptyBox.Item1, EmptyBox.Item2] = i;

                if (SolvePuzzle(derivedSolution))
                {
                    return true;
                }
                derivedSolution[EmptyBox.Item1, EmptyBox.Item2] = 0;
            }
        }

        return false;
    }

    public void Validate()
    {
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                if (derivedSolution[i, j] != numbersInPuzzle[i, j])
                {
                    Debug.Log("False in Validate");
                    return;
                }
                    
            }
        }
        Debug.Log("Validated.");
        timer.StopTimer();
        return;
    }

    Tuple<int, int> FindEmptySpace()
    {
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                if (derivedSolution[i, j] == 0)
                    return new Tuple<int, int>(i, j);
            }
        }
        return null;
    }

    bool IsValid(int toCheck, Tuple<int, int> rowColumn)
    {

        int row = rowColumn.Item1;
        int column = rowColumn.Item2;

        // Checking Row by looping across
        for (int i = 0; i < 9; i++)
        {
            if (derivedSolution[row, i] == toCheck && column != i)
                return false;
        }

        // Checking Column by looping column

        for (int j = 0; j < 9; j++)
        {
            if (derivedSolution[j, column] == toCheck && row != j)
                return false;
        }

        // Check for box

        int BoxColumn = column / 3;
        int BoxRow = row / 3;

        for (int i = BoxRow * 3; i < (BoxRow * 3) + 3; i++)
        {
            for (int j = BoxColumn * 3; j < (BoxColumn * 3) + 3; j++)
            {
                if (derivedSolution[i, j] == toCheck && i != row && j != column)
                    return false;
            }
        }

        return true;
    }

    public Tuple<int, int> FindEmptyBoxForArrows()
    {
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                if (numbersInPuzzle[i, j] == 0)
                    return new Tuple<int, int>(i, j);
            }
        }
        return null;
    }


}
