using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    bool selected = false;
    bool value = false;
    [SerializeField] private Text num;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            selected = true;
            Debug.Log("Pressed left click");
        }

        if (Input.GetKey(KeyCode.DownArrow))
            selected = false;

        if (selected)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                value = true;
                enterNumber(1);
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                value = true;
                enterNumber(2);
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                value = true;
                enterNumber(3);
            }
            if (Input.GetKey(KeyCode.Alpha4))
            {
                value = true;
                enterNumber(4);
            }
            if (Input.GetKey(KeyCode.Alpha5))
            {
                value = true;
                enterNumber(5);
            }
            if (Input.GetKey(KeyCode.Alpha6))
            {
                value = true;
                enterNumber(6);
            }
            if (Input.GetKey(KeyCode.Alpha7))
            {
                value = true;
                enterNumber(7);
            }
            if (Input.GetKey(KeyCode.Alpha8))
            {
                value = true;
                enterNumber(8);
            }
            if (Input.GetKey(KeyCode.Alpha9))
            {
                value = true;
                enterNumber(9);
            }
            if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Backspace) || Input.GetKey(KeyCode.Delete))
                DeleteValue();

        }
    }

    void enterNumber(int toEnter)
    {
        Debug.Log($"Pressed {toEnter}");

        num.text = toEnter.ToString();

        selected = false;
        value = false;
    }

    void DeleteValue()
    {
        Debug.Log("Delete");
        num.text = "";
        selected = false;
    }
}
