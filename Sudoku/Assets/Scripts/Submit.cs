using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submit : MonoBehaviour
{
    [SerializeField] PuzzleManager puzzleManager;

    public void OnClick()
    {
        puzzleManager.Validate();
    }


}
