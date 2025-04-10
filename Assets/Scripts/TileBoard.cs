using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public Tile tilePrefab;
    public TileState[] tileStates;
    TileGrid tileGrid;
    List<Tile> tiles;

    void Awake()
    {
        tileGrid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(tileGrid.size);
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnTile();
        SpawnTile();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) { MoveTile(Vector2Int.up, 0, 1, 1, 1); }
        else if (Input.GetKeyDown(KeyCode.S)) { MoveTile(Vector2Int.down, 0, 1, tileGrid.height - 2, -1); }
        else if (Input.GetKeyDown(KeyCode.A)) { MoveTile(Vector2Int.left, 1, 1, 0, 1); }
        else if (Input.GetKeyDown(KeyCode.D)) { MoveTile(Vector2Int.right, tileGrid.width - 2, -1, 0, 1); }
    }
    public void MoveTile(Vector2Int direction, int startX, int incrementX, int startY, int incrementY) //查詢範圍內的所有cell
    {
        for (int x = startX; x >= 0 && x < tileGrid.width; x += incrementX) //左至右
        {
            for (int y = startY; y >= 0 && y < tileGrid.height; y += incrementY)    //上至下
            {
                TileCell cell = tileGrid.GetCell(x, y);

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
            if (adjacentCell.occupied)
            {
                //合併
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
}
