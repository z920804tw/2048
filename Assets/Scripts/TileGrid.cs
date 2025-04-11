using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows;
    public TileCell[] cells;

    public int size;
    public int height;
    public int width;

    void Awake()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();

        size = cells.Length;
        height = rows.Length;
        width = size / height;
    }
    // Start is called before the first frame update
    void Start()
    {
        //初始化每個Row底下的Cell編號 ，如第一排(0,0) (1,0) (2,0) (3,0) 第二排 (0,1) (1,1) (2,1) (3,1)..... 
        for (int y = 0; y < height; y++) //Row行(上至下)
        {
            for (int x = 0; x < width; x++) //Col列(左至右)
            {
                rows[y].cells[x].coordinates = new Vector2Int(x, y);
            }
        }
    }

    public TileCell GetCell(int x, int y) //取得目標cell
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return rows[y].cells[x]; //回傳目標cell
        }
        else
        {
            return null;
        }
    }

    public TileCell GetAdjacentCell(TileCell cell, Vector2Int dir) //取得該cell的旁邊方向的cell
    {
        Vector2Int coordinates= cell.coordinates; //取得該cell的座標
        coordinates.x+=dir.x;
        coordinates.y-=dir.y;

        return GetCell(coordinates.x,coordinates.y); //回傳隔壁的格子的座標給GetCell
    }

    public TileCell GetRandomEmptyCell() //取的隨機的未被占用cell
    {
        int index = Random.Range(0, size); //設定隨機數
        int startIndex = index;             //紀錄起始位置
        while (cells[index].occupied)       //跑回圈，判斷該位置有沒有被占用，如果有就繼續找下一個
        {
            index++;
            if (index >= size)              //當找到cell的最後面時，會回到第一個繼續找
            {
                index = 0;
            }
            if (index == startIndex)        //當cell的數值等於起始位置，就會回傳null，代表全部位置被占用
            {
                return null;
            }
        }


        return cells[index];
    }
}
