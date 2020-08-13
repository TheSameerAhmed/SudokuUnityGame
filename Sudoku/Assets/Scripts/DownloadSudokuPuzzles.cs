using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class DownloadSudokuPuzzles 
{
    public static List<int[,]> ParsePuzzles(string fileName)
    {
        List<int[,]> output = new List<int[,]>();

        string[] rows = File.ReadAllLines($@"{Application.streamingAssetsPath}/SavedPuzzles/{fileName}");

        int last = 0;
        int n = 0;
        while (last < rows.Length)
        {
            string s = rows[last];

            if (s.Length < 9)
            {
                last++;
                continue;
            }

            output.Add(new int[9, 9]);

            for (int i = 0; i < 9; i++)
            {
                s = rows[last];
                for (int j = 0; j < 9; j++)
                {
                    output[n][i, j] = s[j] - '0';
                    Debug.Log(s[j]);
                }
                last++;
            }
            n++;
        }
        return output;
    }

}
