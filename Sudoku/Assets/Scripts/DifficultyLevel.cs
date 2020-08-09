using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyLevel : MonoBehaviour
{

    [SerializeField] NextAndPrevious nextAndPrevious;

    public void OnClickEasy()
    {
        nextAndPrevious.difficulty = 1;
        AudioManager.instance.PlayButtonClick();
        nextAndPrevious.DisplayPuzzle();
    }

    public void OnClickMedium()
    {
        nextAndPrevious.difficulty = 2;
        AudioManager.instance.PlayButtonClick();
        nextAndPrevious.DisplayPuzzle();
    }
    public void OnClickHard()
    {
        nextAndPrevious.difficulty = 3;
        AudioManager.instance.PlayButtonClick();
        nextAndPrevious.DisplayPuzzle();
    }

    public void OnClickBack()
    {
        AudioManager.instance.PlayButtonClick();
        SceneManager.LoadScene(0);
    }
}
