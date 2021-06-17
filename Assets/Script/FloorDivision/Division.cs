using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//生成の流れ
//まず縦横の長さを調べ、長い方を分割する
//分割して狭くなった方に部屋を作る
//生成された部屋の四方に分割ラインを探して検索できたらそこまで道を生やす
//生やした道から分割ライン上を検索して他の部屋から生えてる道を探して検索できたら道どうしを繋げる
//分割して広くなったほうをまた分割する
//ここまでを指定した部屋の数になるまで行う
//(残った分割ラインを消す)
public class Division : MonoBehaviour
{
    [SerializeField] int min_v;　       //　マップを生成する範囲の左端
    [SerializeField] int min_h;         //　マップを生成する範囲の上端
    [SerializeField] int max_v;         //　マップを生成する範囲の右端
    [SerializeField] int max_h;         //　マップを生成する範囲の下端
    [SerializeField] Cell[,] cellArray; //　生成するセルを格納する配列
    [SerializeField] Cell cellPrefab;   //　生成するセル
    [SerializeField] int roomNum;       //　分割する部屋の数
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
        //for (int i = 0; i < cellArray.GetLength(0); i++)
        //{
        //    for (int k = 0; k < cellArray.GetLength(1); k++)
        //    {
        //        if (cellArray[i, k].cellState == CellState.divisionLine)
        //        {
        //            cellArray[i, k].cellState = CellState.wall;
        //        }
        //    }
        //}
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
            roomCreate(minV, minH, maxV - 1, maxH - 1, cells);
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
                roomCreate(delimitLine + 1, minH, maxV - 1, maxH - 1, cells);
                Delimit(minV, minH, delimitLine, maxH, cells, room);
            }
            else
            {
                roomCreate(minV, minH, delimitLine - 1, maxH - 1, cells);
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
                roomCreate(minV, delimitLine + 1, maxV - 1, maxH - 1, cells);
                Delimit(minV, minH, maxV, delimitLine, cells, room);
            }
            else
            {
                roomCreate(minV, minH, maxV - 1, delimitLine - 1, cells);
                Delimit(minV, delimitLine + 1, maxV, maxH, cells, room);
            }
        }
    }

    /// <summary>
    /// 部屋の生成
    /// </summary>
    /// <param name="minV">分割された空間の左端</param>
    /// <param name="minH">分割された空間の上端</param>
    /// <param name="maxV">分割された空間の右端</param>
    /// <param name="maxH">分割された空間の下端</param>
    /// <param name="cells">配列</param>
    void roomCreate(int minV, int minH, int maxV, int maxH, Cell[,] cells)
    {
        for (int i = minV + 1; i < maxV; i++)
        {
            for (int k = minH + 1; k < maxH; k++)
            {
                cells[i, k].cellState = CellState.floor;
            }
        }
        WayCreate(minV, minH, maxV, maxH, cells);
    }

    /// <summary>
    /// 部屋から道を伸ばす
    /// </summary>
    /// <param name="minV">部屋の左端 - 1</param>
    /// <param name="minH">部屋の上端 - 1</param>
    /// <param name="maxV">部屋の右端 + 1</param>
    /// <param name="maxH">部屋の下端 + 1</param>
    /// <param name="cells">配列</param>
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
                    WayConect(i, minVside, cells, true);
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
                    WayConect(i, maxVside, cells, true);
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
                    WayConect(i, minHside, cells, false);
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
                    WayConect(i, maxHside, cells, false);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 部屋から伸ばした道通しを繋げる
    /// </summary>
    /// <param name="i">カウンタ変数(for分の中で呼び出すため)</param>
    /// <param name="side">部屋から伸びた道の方向</param>
    /// <param name="cells">配列</param>
    /// <param name="isVirtical">縦方向を調べるか否か</param>
    void WayConect(int i, int side, Cell[,] cells, bool isVirtical)
    {
        if (isVirtical == true)
        {
            for (int k = side; k >= 0; k--)
            {
                if (cells[k, i].cellState == CellState.wall || k == 0)
                {
                    for (int m = side - 1; m > k + 1; m--)
                    {
                        if (cells[m, i].cellState == CellState.floor)
                        {
                            for (int n = side - 1; n >= m; n--)
                            {
                                cells[n, i].cellState = CellState.floor;
                            }
                        }
                    }
                    break;
                }
            }
            for (int k = side; k < cells.GetLength(0); k++)
            {
                if (cells[k, i].cellState == CellState.wall || k == cells.GetLength(0) - 1)
                {
                    for (int m = side + 1; m < k - 1; m++)
                    {
                        if (cells[m, i].cellState == CellState.floor)
                        {
                            for (int n = side + 1; n <= m; n++)
                            {
                                cells[n, i].cellState = CellState.floor;
                            }
                        }
                    }
                    break;
                }
            }
        }
        else
        {
            for (int k = side; k >= 0; k--)
            {
                if (cells[i, k].cellState == CellState.wall || k == 0)
                {
                    for (int m = side - 1; m > k + 1; m--)
                    {
                        if (cells[i, m].cellState == CellState.floor)
                        {
                            for (int n = side - 1; n >= m; n--)
                            {
                                cells[i, n].cellState = CellState.floor;
                            }
                        }
                    }
                    break;
                }
            }
            for (int k = side; k < cells.GetLength(1); k++)
            {
                if (cells[i, k].cellState == CellState.wall || k == cells.GetLength(1) - 1)
                {
                    for (int m = side + 1; m < k - 1; m++)
                    {
                        if (cells[i, m].cellState == CellState.floor)
                        {
                            for (int n = side + 1; n <= m; n++)
                            {
                                cells[i, n].cellState = CellState.floor;
                            }
                        }
                    }
                    break;
                }
            }
        }
    }
}
