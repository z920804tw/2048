using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public GameManager gameManager;
    public Tile tilePrefab;
    public TileState[] tileStates;
    TileGrid tileGrid;
    List<Tile> tiles;

    public bool canMove;
    void Awake()
    {
        tileGrid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(tileGrid.size);
    }


    // Update is called once per frame
    void Update()
    {
        if (!canMove) return; //如果目前canMove=false就直接跳過下面的程式碼

        if (Input.GetKeyDown(KeyCode.W)) //上
        {
            CheckCell(Vector2Int.up, 0, 1, 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S)) //下
        {
            CheckCell(Vector2Int.down, 0, 1, tileGrid.height - 2, -1);
        }
        else if (Input.GetKeyDown(KeyCode.A))//左
        {
            CheckCell(Vector2Int.left, 1, 1, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.D))   //右
        {
            CheckCell(Vector2Int.right, tileGrid.width - 2, -1, 0, 1);
        }

    }
    public void CheckCell(Vector2Int direction, int startX, int incrementX, int startY, int incrementY) //查詢範圍內的所有cell
    {
        StartCoroutine(WaitTileChange()); //等待所有方塊的移動
        for (int x = startX; x >= 0 && x < tileGrid.width; x += incrementX) //左至右
        {
            for (int y = startY; y >= 0 && y < tileGrid.height; y += incrementY)    //上至下
            {
                TileCell cell = tileGrid.GetCell(x, y); //取得目標cell

                if (cell.occupied) //如果該位置有被占用，就移動該格子的tile位置
                {
                    MoveTilePos(cell.tile, direction);
                }
            }
        }
    }
    public void MoveTilePos(Tile tile, Vector2Int dir) //adjacent=鄰近的 newCell=最後存取的位置cell
    {
        TileCell newCell = null;
        TileCell adjacentCell = tileGrid.GetAdjacentCell(tile.cell, dir);

        while (adjacentCell != null)
        {
            if (adjacentCell.occupied) //如果鄰近的cell有被佔領，就接著判斷數值是不是一樣，是否要合併
            {
                //合併
                if (CanMergeTiles(tile, adjacentCell.tile)) //判斷能不能合併
                {
                    MergeTile(tile, adjacentCell.tile);
                }

                break;
            }
            newCell = adjacentCell;
            adjacentCell = tileGrid.GetAdjacentCell(adjacentCell, dir); //繼續往某個方向跑
        }

        if (newCell != null)
        {
            tile.MoveTo(newCell);
        }
    }

    bool CanMergeTiles(Tile a, Tile b) //判斷要不要合併，和有沒有被鎖住，以防一次合併多個
    {
        if (a.number == b.number && !b.lockTile)
        {
            return true;
        }
        else return false;
    }
    void MergeTile(Tile a, Tile b) //實際的合併方塊
    {
        tiles.Remove(a);        //先將要被合併的方塊移出list清單

        a.MergeCell(b.cell); //a方塊會往b方塊移動

        StartCoroutine(MergeDelay(b)); //延遲B數值的更新時間
    }

    int IndexOfTile(TileState tileState) //取得要更新的方塊state
    {
        for (int i = 0; i < tileStates.Length; i++)
        {
            if (tileState == tileStates[i])
            {
                return i;
            }
        }
        return 99;

    }
    public void ClearBoard() //清空Board上所有設定
    {
        //清空每個Cell紀錄的tile
        foreach (var cell in tileGrid.cells)
        {
            cell.tile = null;
        }

        //清空場上所有的tile物件
        foreach (var tile in tiles)
        {
            Destroy(tile.gameObject);
        }
        tiles.Clear();
    }

    public void SpawnTile()
    {
        TileCell tileCell = tileGrid.GetRandomEmptyCell(); //先取得沒有被占位的cell位置
        if (tileCell != null)                       //確保回傳的tileCell有東西才會生成方塊
        {
            Tile tile = Instantiate(tilePrefab, tileGrid.transform);
            tile.SetTileState(tileStates[0]); //設定磚塊的基礎數值(預設2)
            tile.SetTilePos(tileCell); //將取得到的cell傳入

            tiles.Add(tile);
        }
    }

    IEnumerator MergeDelay(Tile b) //數值延遲
    {

        yield return new WaitForSeconds(0.1f);

        int index = Mathf.Clamp(IndexOfTile(b.tileState) + 1, 0, tileStates.Length - 1); //限制回傳的數值最大只會是tileStates陣列的大小
        b.tileState = tileStates[index];
        b.number = b.number * 2;

        b.SetTileState(tileStates[index]);

    }

    IEnumerator WaitTileChange() //操作、方塊生成延遲
    {
        canMove = false;

        yield return new WaitForSeconds(0.2f);

        canMove = true;

        foreach (Tile tile in tiles)
        {
            tile.lockTile = false;
        }

        //檢查是否結束
        if (CheckGameOver())
        {
            gameManager.GameOver();
        }
    }

    bool CheckGameOver()
    {
        if (tiles.Count != tileGrid.size) //如果不是最大，那就回傳false，表示還沒結束
        {

            SpawnTile();
            return false;
        }
        else                            //如果已經滿了，就檢查每個tile的鄰近目標能不能合併
        {
            foreach (var tile in tiles)
            {
                TileCell up = tileGrid.GetAdjacentCell(tile.cell, Vector2Int.up); //上
                TileCell down = tileGrid.GetAdjacentCell(tile.cell, Vector2Int.down); //下
                TileCell left = tileGrid.GetAdjacentCell(tile.cell, Vector2Int.left); //左
                TileCell right = tileGrid.GetAdjacentCell(tile.cell, Vector2Int.right); //右

                //如果只要有一個方向可以合併，就代表還沒結束，否則如果都沒有就回傳true，代表結束
                if (up != null && CanMergeTiles(tile, up.tile)
                || down != null && CanMergeTiles(tile, down.tile)
                || left != null && CanMergeTiles(tile, left.tile)
                || right != null && CanMergeTiles(tile, right.tile))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
