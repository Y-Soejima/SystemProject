using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    wall,
    floor,
    divisionLine
}
public class Cell : MonoBehaviour
{
    Image image;
    public CellState cellState;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        CellStateChange();
    }

    void CellStateChange()
    {
        if (cellState == CellState.wall)
        {
            image.color = Color.gray;
        }
        else if (cellState == CellState.floor)
        {
            image.color = Color.red;
        }
        else if (cellState== CellState.divisionLine)
        {
            image.color = Color.blue;
        }
    }
}
