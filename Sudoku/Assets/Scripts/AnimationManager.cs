using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    [SerializeField] GameObject puzzleCompleteAnimation;
    [SerializeField] GameObject puzzleInCompleteAnimation;
    [SerializeField] CellScript1 cells;

    public void PuzzleCompleted()
    {
        puzzleCompleteAnimation.SetActive(true);
        cells.LockCompletedPuzzle();
        Invoke("EndAnimation", 3f);
    }

    public void PuzzleNotCompleted()
    {
        puzzleInCompleteAnimation.SetActive(true);
        Invoke("EndInCompleteAnimation", 0.5f);
    }

    void EndInCompleteAnimation()
    {
        puzzleInCompleteAnimation.SetActive(false);
    }
    void EndAnimation()
    {
        puzzleCompleteAnimation.SetActive(false);
    }

}
