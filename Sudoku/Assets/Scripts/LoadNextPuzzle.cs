using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextPuzzle : MonoBehaviour
{

    [SerializeField] NextAndPrevious nextAndPrevious;

    public void LoadOtherPuzzle()
    {
        nextAndPrevious.NextPuzzle();
    }


}
