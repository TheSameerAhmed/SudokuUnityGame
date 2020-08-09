using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Submit : MonoBehaviour
{
    [SerializeField] PuzzleManager puzzleManager;
    [SerializeField] NextAndPrevious nextAndPrevious;
    

    void Update()
    {
        if (nextAndPrevious.savedValidationState[nextAndPrevious.index] == true)
            this.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        else
            this.transform.GetChild(0).GetComponent<Text>().color = new Color(0f,1f, 0.1975842f,1f);
    }   

    public void OnClick()
    {
        AudioManager.instance.PlayButtonClick();
        puzzleManager.Validate();
    }




}
