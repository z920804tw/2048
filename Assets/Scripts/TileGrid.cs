using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows;
    public TileCell[] cells;


    public TileState tileStates;
    public int size;
    public int height;
    public int width;

    // Start is called before the first frame update

    void Awake()
    {

    }
    void Start()
    {

        // rows = GetComponentsInChildren<TileRow>();
        // cells = GetComponentsInChildren<TileCell>();

        size = cells.Length;
        height = rows.Length;
        width = size / height;
    }

    void Update()
    {

    }
    public TileCell GetCell(int x, int y)
    {

        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return rows[y].cells[x];
        }
        else
        {
            return null;
        }

    }
    public TileCell GetCell1(Vector2Int dir)
    {
        return GetCell(dir.x, dir.y);
    }
    public TileCell GetAdjacentCell(TileCell cell, Vector2Int dir)
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += dir.x;
        coordinates.y -= dir.y;

        return GetCell1(coordinates);
    }


    public TileCell GetRandomEmptyCell()
    {
        //隨機取Cell
        int index = Random.Range(0, cells.Length);
        int startIndex = index;
        while (cells[index].occupied)
        {
            index++;
            //如果index到結尾就回到第一個
            if (index >= cells.Length)
            {
                index = 0;
            }

            //如果index回到初始點就回傳Null
            if (index == startIndex)
            {
                return null;
            }
        }
        return cells[index];
    }


}
