using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyLevel : MonoBehaviour
{

    [SerializeField] NextAndPrevious nextAndPrevious;

    public void OnClickEasy()
    {
        nextAndPrevious.difficulty = 1;
        nextAndPrevious.DisplayPuzzle();
    }

    public void OnClickMedium()
    {
        nextAndPrevious.difficulty = 2;
        nextAndPrevious.DisplayPuzzle();
    }
    public void OnClickHard()
    {
        nextAndPrevious.difficulty = 3;
        nextAndPrevious.DisplayPuzzle();
    }

    public void OnClickBack()
    {
        SceneManager.LoadScene(0);
    }
}
