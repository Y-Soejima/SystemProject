using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Division : MonoBehaviour
{
    [SerializeField] int min_v;
    [SerializeField] int min_h;
    [SerializeField] int max_v;
    [SerializeField] int max_h;
    [SerializeField] Cell[,] cellArray;
    [SerializeField] Cell cellPrefab;
    [SerializeField] int roomNum;
    [SerializeField] private GridLayoutGroup panel;
    // Start is called before the first frame update
    void Start()
    {
        cellArray = new Cell[max_v, max_h];
        for (int i = 0; i < cellArray.GetLength(0); i++)
        {
            for (int k = 0; k < cellArray.GetLength(1); k++)
            {
                var cell = Instantiate(cellPrefab);
                cell.transform.SetParent(panel.transform);
                cell.cellState = CellState.wall;
                cellArray[i, k] = cell;
            }
        }
        Delimit(min_v, min_h, max_v, max_h, cellArray, roomNum);
    }

    // Update is called once per frame
    void Delimit(int minV, int minH, int maxV, int maxH, Cell[,] cells, int room)
    {
        room--;
        int v_length = maxV - minV;
        int h_length = maxH - minH;
        if (v_length <= 8 && h_length <= 8 || room == 0)
        {
            return;
        }
        int delimitLine = 0;
        if (v_length >= h_length)
        {
            delimitLine = Random.Range(minV + 4, maxV - 4);
            for (int i = minH; i < maxH;i++)
            {
                cells[delimitLine, i].cellState = CellState.floor;
            }
            if (delimitLine >= v_length / 2)
            {
                Delimit(minV, minH, delimitLine, maxH, cells, room);
            }
            else
            {
                Delimit(delimitLine + 1, minH, maxV, maxH, cells, room);
            }
        }
        else
        {
            delimitLine = Random.Range(minH + 4, maxH - 4);
            for (int i = minV; i < maxV; i++)
            {
                cells[i, delimitLine].cellState = CellState.floor;
            }
            if (delimitLine >= h_length / 2)
            {
                Delimit(minV, minH, maxV, delimitLine, cells, room);
            }
            else
            {
                Delimit(minV, delimitLine + 1, maxV, maxH, cells, room);
            }
        }
    }
}
