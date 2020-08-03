using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CellModel
{
    [Range(1,9)]
    public int Number;
    public Text textBox;
    public GameObject gameObject;
}
