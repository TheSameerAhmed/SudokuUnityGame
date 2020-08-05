using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    [SerializeField] GameObject puzzleCompleteAnimation;

    public void PuzzleCompleted()
    {
        puzzleCompleteAnimation.SetActive(true);
        Invoke("EndAnimation", 3f);
    }

    void EndAnimation()
    {
        puzzleCompleteAnimation.SetActive(false);
    }
}
