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
        for (int i = 0; i < cellArray.GetLength(0); i++)
        {
            for (int k = 0; k < cellArray.GetLength(1); k++)
            {
                if (cellArray[i, k].cellState == CellState.divisionLine)
                {
                    cellArray[i, k].cellState = CellState.wall;
                }
            }
        }
    }

    /// <summary>
    /// マップを分割する
    /// </summary>
    /// <param name="minV">分割する範囲の縦方向の最小値</param>
    /// <param name="minH">分割する範囲の横方向の最小値</param>
    /// <param name="maxV">分割する範囲の縦方向の最大値</param>
    /// <param name="maxH">分割する範囲の横方向の最大値</param>
    /// <param name="cells">マップの配列</param>
    /// <param name="room">部屋の数</param>
    void Delimit(int minV, int minH, int maxV, int maxH, Cell[,] cells, int room)
    {
        
        room--;
        int v_length = maxV - minV;
        int h_length = maxH - minH;
        if (v_length <= 8 && h_length <= 8 || room == 0)
        {
            roomCreate(minV, minH, maxV, maxH, cells);
            return;
        }
        int delimitLine = 0;
        if (v_length >= h_length)
        {
            delimitLine = Random.Range(minV + 4, maxV - 4);
            for (int i = minH; i < maxH;i++)
            {
                cells[delimitLine, i].cellState = CellState.divisionLine;
            }
            if (delimitLine >= v_length / 2)
            {
                roomCreate(delimitLine + 1, minH, maxV, maxH, cells);
                Delimit(minV, minH, delimitLine, maxH, cells, room);
            }
            else
            {
                roomCreate(minV, minH, delimitLine, maxH, cells);
                Delimit(delimitLine + 1, minH, maxV, maxH, cells, room);
            }
        }
        else
        {
            delimitLine = Random.Range(minH + 4, maxH - 4);
            for (int i = minV; i < maxV; i++)
            {
                cells[i, delimitLine].cellState = CellState.divisionLine;
            }
            if (delimitLine >= h_length / 2)
            {
                roomCreate(minV, delimitLine + 1, maxV, maxH, cells);
                Delimit(minV, minH, maxV, delimitLine, cells, room);
            }
            else
            {
                roomCreate(minV, minH, maxV, delimitLine, cells);
                Delimit(minV, delimitLine + 1, maxV, maxH, cells, room);
            }
        }
    }

    //部屋の生成
    void roomCreate(int minV, int minH, int maxV, int maxH, Cell[,] cells)
    {
        for (int i = minV + 1; i < maxV - 1; i++)
        {
            for (int k = minH + 1; k < maxH - 1; k++)
            {
                cells[i, k].cellState = CellState.floor;
            }
        }
        WayCreate(minV, minH, maxV - 1, maxH - 1, cells);
    }

    //部屋から道を伸ばす
    void WayCreate(int minV, int minH, int maxV, int maxH, Cell[,] cells)
    {
        int v_length = maxV - minV;
        int h_length = maxH - minH;
        int minVside = 0;
        int maxVside = 0;
        int minHside = 0;
        int maxHside = 0;
        if (v_length > 3)
        {
            minVside = Random.Range(minV + 2, maxV - 2);
            maxVside = Random.Range(minV + 2, maxV - 2);
        }
        if (h_length > 3)
        {
            minHside = Random.Range(minH + 2, maxH - 2);
            maxHside = Random.Range(minH + 2, maxH - 2);
        }

        if (minVside != 0)
        {
            for (int i = minH; i > 0; i--)
            {
                if (cells[minVside, i].cellState == CellState.divisionLine || cells[minVside, i].cellState == CellState.floor)
                {
                    for (int k = minH; k >= i; k--)
                    {
                        cells[minVside, k].cellState = CellState.floor;
                    }
                    for (int k = minVside; k >= 0; k--)
                    {
                        if (cells[k, i].cellState == CellState.wall || k == 0)
                        {
                            for (int m = minVside - 1; m > k + 1; m--)
                            {
                                if (cells[m, i].cellState == CellState.floor)
                                {
                                    for (int n = minVside - 1; n >= m; n--)
                                    {
                                        cells[n, i].cellState = CellState.floor;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    for (int k = minVside; k < cells.GetLength(0); k++)
                    {
                        if (cells[k, i].cellState == CellState.wall || k == cells.GetLength(0) - 1)
                        {
                            for (int m = minVside + 1; m < k - 1; m++)
                            {
                                if (cells[m, i].cellState == CellState.floor)
                                {
                                    for (int n = minVside + 1; n <= m; n++)
                                    {
                                        cells[n, i].cellState = CellState.floor;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
        if (maxVside != 0)
        {
            for (int i = maxH; i < cells.GetLength(1); i++)
            {
                if (cells[maxVside, i].cellState == CellState.divisionLine || cells[maxVside, i].cellState == CellState.floor)
                {
                    for (int k = maxH; k <= i; k++)
                    {
                        cells[maxVside, k].cellState = CellState.floor;
                    }
                    for (int k = maxVside; k >= 0; k--)
                    {
                        if (cells[k, i].cellState == CellState.wall || k == 0)
                        {
                            for (int m = maxVside - 1; m > k + 1; m--)
                            {
                                if (cells[m, i].cellState == CellState.floor)
                                {
                                    for (int n = maxVside - 1; n >= m; n--)
                                    {
                                        cells[n, i].cellState = CellState.floor;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    for (int k = maxVside; k < cells.GetLength(0); k++)
                    {
                        if (cells[k, i].cellState == CellState.wall || k == cells.GetLength(0) - 1)
                        {
                            for (int m = maxVside + 1; m < k - 1; m++)
                            {
                                if (cells[m, i].cellState == CellState.floor)
                                {
                                    for (int n = maxVside + 1; n <= m; n++)
                                    {
                                        cells[n, i].cellState = CellState.floor;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
        if (minHside != 0)
        {
            for (int i = minV; i > 0; i--)
            {
                if (cells[i, minHside].cellState == CellState.divisionLine || cells[i, minHside].cellState == CellState.floor)
                {
                    for (int k = minV; k >= i; k--)
                    {
                        cells[k, minHside].cellState = CellState.floor;
                    }
                    for (int k = minHside; k >= 0; k--)
                    {
                        if (cells[i, k].cellState == CellState.wall || k == 0)
                        {
                            for (int m = minHside - 1; m > k + 1; m--)
                            {
                                if (cells[i, m].cellState == CellState.floor)
                                {
                                    for (int n = minHside - 1; n >= m; n--)
                                    {
                                        cells[i, n].cellState = CellState.floor;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    for (int k = minHside; k < cells.GetLength(1); k++)
                    {
                        if (cells[i, k].cellState == CellState.wall || k == cells.GetLength(1) - 1)
                        {
                            for (int m = minHside + 1; m < k - 1; m++)
                            {
                                if (cells[i, m].cellState == CellState.floor)
                                {
                                    for (int n = minHside + 1; n <= m; n++)
                                    {
                                        cells[i, n].cellState = CellState.floor;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
        if (maxHside != 0)
        {
            for (int i = maxV; i < cells.GetLength(0); i++)
            {
                if (cells[i, maxHside].cellState == CellState.divisionLine || cells[i, maxHside].cellState == CellState.floor)
                {
                    for (int k = maxV; k <= i; k++)
                    {
                        cells[k, maxHside].cellState = CellState.floor;
                    }
                    for (int k = maxHside; k >= 0; k--)
                    {
                        if (cells[i, k].cellState == CellState.wall || k == 0)
                        {
                            for (int m = maxHside - 1; m > k + 1; m--)
                            {
                                if (cells[i, m].cellState == CellState.floor)
                                {
                                    for (int n = maxHside - 1; n >= m; n--)
                                    {
                                        cells[i, n].cellState = CellState.floor;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    for (int k = maxHside; k < cells.GetLength(1); k++)
                    {
                        if (cells[i, k].cellState == CellState.wall || k == cells.GetLength(1) - 1)
                        {
                            for (int m = maxHside + 1; m < k - 1; m++)
                            {
                                if (cells[i, m].cellState == CellState.floor)
                                {
                                    for (int n = maxHside + 1; n <= m; n++)
                                    {
                                        cells[i, n].cellState = CellState.floor;
                                    }
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}
