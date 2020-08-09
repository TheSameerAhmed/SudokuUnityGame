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
        AudioManager.instance.Play("Solved");
        cells.LockCompletedPuzzle();
        Invoke("EndAnimation", 3f);
    }

    public void PuzzleNotCompleted()
    {
        puzzleInCompleteAnimation.SetActive(true);        
        AudioManager.instance.Play("NotSolved");
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
