using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class CellScript1 : MonoBehaviour
{
    [SerializeField] PuzzleManager puzzleManager;
    [SerializeField] Canvas mainCanvas;

    bool selected = false;
    bool value = false;
    Text selectedTextbox;
    string selectedIndex;
    GameObject selectedBox;

    int[,] model = new int[9, 9] { {0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0},};

    public HashSet<Tuple<int, int>> fixedValues;
    bool up, down, left, right;

    public void LoadSavedPuzzle(int[,] savedSudokuPuzzle, HashSet<Tuple<int, int>> savedFixedValues, int[,] originalPuzzle)
    {
        UnHighlightBox();
        selected = false;
        value = false;
        selectedTextbox = null;
        selectedIndex = null;
        selectedBox = null;
        fixedValues = savedFixedValues;

        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                model[i, j] = savedSudokuPuzzle[i, j];
                if(fixedValues.Contains(new Tuple<int,int>(i , j)))
                {
                    EnterPreDefinedValues(savedSudokuPuzzle[i, j], $"{i}{j}");
                }
                else if(savedSudokuPuzzle[i,j] != 0)
                {
                    mainCanvas.transform.Find($"{i}{j}").GetComponent<Text>().text = $"{savedSudokuPuzzle[i, j]}";
                }
            }
        }
        puzzleManager.SolvePuzzle(originalPuzzle);
    }

    public void LoadPuzzle(int[,] sudokuPuzzle)
    {
        UnHighlightBox();
        selected = false;
        value = false;
        selectedTextbox = null;
        selectedIndex = null;
        selectedBox = null;
        fixedValues = new HashSet<Tuple<int, int>>();

        Debug.Log($"In LoadPuzzle: {puzzleManager.numbersInPuzzle[0, 4]}");

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                puzzleManager.numbersInPuzzle[i, j] = sudokuPuzzle[i, j];
                model[i, j] = sudokuPuzzle[i, j];
                if (model[i, j] != 0)
                {
                    EnterPreDefinedValues(model[i, j], Convert.ToString(i) + Convert.ToString(j));
                    fixedValues.Add(new Tuple<int, int>(i, j));
                }
            }
        }
        Debug.Log($"Index 2 in script: {puzzleManager.numbersInPuzzle[0, 1]}");
        puzzleManager.SolvePuzzle(model);

    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            selected = true;

            Vector3 position = Input.mousePosition;
            Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(position));

            if(hitCollider != null)
            {
                selectedTextbox = mainCanvas.transform.Find(hitCollider.gameObject.tag).GetComponent<Text>();                
                selectedIndex = hitCollider.gameObject.tag;
                UnHighlightBox();
                selectedBox = hitCollider.gameObject;
                HighlightSelectedBox();
            }
        }

        up = Input.GetKeyUp(KeyCode.UpArrow);
        down = Input.GetKeyUp(KeyCode.DownArrow);
        left = Input.GetKeyUp(KeyCode.LeftArrow);
        right = Input.GetKeyUp(KeyCode.RightArrow);

        if (up || down || right || left)
        {
            selected = true;
            Tuple<int, int> emptyBox;
            if (selectedIndex == null)
            {
                emptyBox = puzzleManager.FindEmptyBoxForArrows();
                selectedIndex = $"{emptyBox.Item1}{emptyBox.Item2}";
                selectedTextbox = mainCanvas.transform.Find($"{emptyBox.Item1}{emptyBox.Item2}").GetComponent<Text>();
                UnHighlightBox();
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag($"{emptyBox.Item1}{emptyBox.Item2}"))
                {
                    if (obj.transform.childCount == 1)
                        selectedBox = obj;
                }
                HighlightSelectedBox();
                up = down = right = left = false;
            }
            else
            {
                KeyBoardControls();
            }
        }

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
        Tuple<int, int> toCheck = new Tuple<int, int>(Convert.ToInt32(selectedIndex.Substring(0, 1)), Convert.ToInt32(selectedIndex.Substring(1, 1)));
        if (selectedTextbox != null && !fixedValues.Contains(toCheck))
        {
            Debug.Log($"Pressed {toEnter}");
            selectedTextbox.text = toEnter.ToString();
            puzzleManager.InsertValue(selectedIndex, toEnter);
            selected = true;
            value = false;
        }
    }

    void DeleteValue()
    {
        Tuple<int, int> toCheck = new Tuple<int, int>(Convert.ToInt32(selectedIndex.Substring(0, 1)), Convert.ToInt32(selectedIndex.Substring(1, 1)));
        if (selectedTextbox != null && !fixedValues.Contains(toCheck))
        {
            selectedTextbox.text = "";
            //selected = false;
            //selectedTextbox = null;
            puzzleManager.DeleteValue(selectedIndex);
            //selectedIndex = null;
        }
    }

    void EnterPreDefinedValues(int toEnter, string index)
    {
        selectedTextbox = mainCanvas.transform.Find(index).GetComponent<Text>();
        selectedTextbox.text = toEnter.ToString();
        selectedTextbox.color = new Color(0.61f, 0.66f, 0.7f);
        
        selectedTextbox = null;
    }

    void UnHighlightBox()
    {
        if (selectedBox != null)
           selectedBox.GetComponent<SpriteRenderer>().color = Color.black;
    }

    void HighlightSelectedBox()
    {
        Tuple<int, int> toCheck = new Tuple<int, int>(Convert.ToInt32(selectedBox.tag.Substring(0, 1)), Convert.ToInt32(selectedBox.tag.Substring(1, 1)));
        if (!fixedValues.Contains(toCheck))
            selectedBox.GetComponent<SpriteRenderer>().color = Color.red;
    }

    void KeyBoardControls()
    {
        if (up && Convert.ToInt32(selectedBox.tag.Substring(0, 1)) - 1 >= 0)
        {
            selected = true;
            Tuple<int, int> toCheck = FindEmptyBoxUp(Convert.ToInt32(selectedBox.tag.Substring(0, 1)) - 1, Convert.ToInt32(selectedBox.tag.Substring(1, 1)));
            if (toCheck != null)
            {
                int newRow = toCheck.Item1;
                int newCol = toCheck.Item2;
                Debug.Log($"Inside up {newRow},{newCol}");
                selectedTextbox = mainCanvas.transform.Find($"{newRow}{newCol}").GetComponent<Text>();
                selectedIndex = $"{newRow}{newCol}";
                UnHighlightBox();
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag($"{newRow}{newCol}"))
                {
                    if (obj.transform.childCount == 1)
                        selectedBox = obj;
                }

                HighlightSelectedBox();
            }
            up = down = right = left = false;
        }
        else if (down && Convert.ToInt32(selectedBox.tag.Substring(0, 1)) + 1 < 9)
        {
            selected = true;
            Tuple<int, int> toCheck = FindEmptyBoxDown(Convert.ToInt32(selectedBox.tag.Substring(0, 1)) + 1, Convert.ToInt32(selectedBox.tag.Substring(1, 1)));
            if (toCheck != null)
            {
                int newRow = toCheck.Item1;
                int newCol = toCheck.Item2;
                Debug.Log($"Inside up {newRow},{newCol}");
                selectedTextbox = mainCanvas.transform.Find($"{newRow}{newCol}").GetComponent<Text>();
                selectedIndex = $"{newRow}{newCol}";
                UnHighlightBox();
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag($"{newRow}{newCol}"))
                {
                    if (obj.transform.childCount == 1)
                        selectedBox = obj;
                }

                HighlightSelectedBox();
            }
            up = down = right = left = false;
        }
        else if(right && Convert.ToInt32(selectedBox.tag.Substring(1, 1)) + 1 < 9)
        {
            selected = true;
            Tuple<int, int> toCheck = FindEmptyBoxRight(Convert.ToInt32(selectedBox.tag.Substring(0, 1)), Convert.ToInt32(selectedBox.tag.Substring(1, 1)) + 1);
            if (toCheck != null)
            {
                int newRow = toCheck.Item1;
                int newCol = toCheck.Item2;
                Debug.Log($"Inside up {newRow},{newCol}");
                selectedTextbox = mainCanvas.transform.Find($"{newRow}{newCol}").GetComponent<Text>();
                selectedIndex = $"{newRow}{newCol}";
                UnHighlightBox();
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag($"{newRow}{newCol}"))
                {
                    if (obj.transform.childCount == 1)
                        selectedBox = obj;
                }

                HighlightSelectedBox();
            }
            up = down = right = left = false;
        }
        else if (left && Convert.ToInt32(selectedBox.tag.Substring(1, 1)) - 1 >= 0)
        {
            selected = true;
            Tuple<int, int> toCheck = FindEmptyBoxLeft(Convert.ToInt32(selectedBox.tag.Substring(0, 1)), Convert.ToInt32(selectedBox.tag.Substring(1, 1)) - 1);
            if (toCheck != null)
            {
                int newRow = toCheck.Item1;
                int newCol = toCheck.Item2;
                Debug.Log($"Inside up {newRow},{newCol}");
                selectedTextbox = mainCanvas.transform.Find($"{newRow}{newCol}").GetComponent<Text>();
                selectedIndex = $"{newRow}{newCol}";
                UnHighlightBox();
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag($"{newRow}{newCol}"))
                {
                    if (obj.transform.childCount == 1)
                        selectedBox = obj;
                }

                HighlightSelectedBox();
            }
            up = down = right = left = false;
        }
    }

    Tuple<int, int> FindEmptyBoxUp(int row, int col)
    {
        for(int i = row; i >= 0; i--)
        {
            if(!fixedValues.Contains(new Tuple<int, int>(i, col)))
            {
                return new Tuple<int, int>(i, col);
            }
        }
        return null;

    }

    Tuple<int, int> FindEmptyBoxDown(int row, int col)
    {
        for (int i = row; i < 9; i++)
        {
            if (!fixedValues.Contains(new Tuple<int, int>(i, col)))
            {
                return new Tuple<int, int>(i, col);
            }
        }
        return null;

    }


    Tuple<int, int> FindEmptyBoxRight(int row, int col)
    {
        for (int i = col; i < 9; i++)
        {
            if (!fixedValues.Contains(new Tuple<int, int>(row, i)))
            {
                return new Tuple<int, int>(row, i);
            }
        }
        return null;

    }

    Tuple<int, int> FindEmptyBoxLeft(int row, int col)
    {
        for (int i = col; i >= 0; i--)
        {
            if (!fixedValues.Contains(new Tuple<int, int>(row, i)))
            {
                return new Tuple<int, int>(row, i);
            }
        }
        return null;

    }

    public void WipePuzzleForNew()
    {
        for(int i = 0; i < 9; i++)
        {
            for(int j = 0; j < 9; j++)
            {
                mainCanvas.transform.Find($"{i}{j}").GetComponent<Text>().text = "";
                mainCanvas.transform.Find($"{i}{j}").GetComponent<Text>().color = Color.black;
            }
        }
    }

    public void LockCompletedPuzzle()
    {
        UnHighlightBox();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                fixedValues.Add(new Tuple<int, int>(i, j));
                mainCanvas.transform.Find($"{i}{j}").GetComponent<Text>().color = Color.green;
            }

        }
    }

    public void LoadCompletedPuzzle()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                mainCanvas.transform.Find($"{i}{j}").GetComponent<Text>().color = Color.green;
            }

        }
    }

    }
