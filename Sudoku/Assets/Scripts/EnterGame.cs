using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGame : MonoBehaviour
{
    
    public void PlayGame()
    {                
        SceneManager.LoadScene(1);
    }

    public void ButtonClick()
    {
        AudioManager.instance.PlayButtonClick();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
