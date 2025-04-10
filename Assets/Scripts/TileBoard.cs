using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public Tile tilePrefab;
    public TileState[] tileStates;
    TileGrid tileGrid;
    List<Tile> tiles;
    // Start is called before the first frame update
    void Start()
    {
        tileGrid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(tileGrid.cells.Length);

        SpawnTile();
        SpawnTile();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) { MoveTiles(Vector2Int.up, 0, 1, 1, 1); }
        else if (Input.GetKeyDown(KeyCode.S)) { MoveTiles(Vector2Int.down, 0, 1, tileGrid.height - 2, -1); }
        else if (Input.GetKeyDown(KeyCode.A)) { MoveTiles(Vector2Int.left, 1, 1, 0, 1); }
        else if (Input.GetKeyDown(KeyCode.D)) { MoveTiles(Vector2Int.right, tileGrid.width - 2, -1, 0, 1); }
    }
    public void SpawnTile()
    {

        TileCell rndCell = tileGrid.GetRandomEmptyCell();
        if (rndCell != null)
        {
            //生成方塊，並設定位置
            Tile tile = Instantiate(tilePrefab, tileGrid.transform);
            tile.SetTile(rndCell);
            tile.SetState(tileStates[0]);

            tiles.Add(tile);
        }
    }


    public void MoveTiles(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {

        for (int x = startX; x >= 0 && x < tileGrid.width; x += incrementX)
        {
            for (int y = startY; y >= 0 && y < tileGrid.height; y += incrementY)
            {
                TileCell cell = tileGrid.GetCell(x, y);
                Debug.Log(cell.occupied);
                if (cell.occupied)
                {
                    MoveTile(cell.tile, direction);
                }

            }
        }
    }

    void MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = tileGrid.GetAdjacentCell(tile.cell, direction);

        while (adjacent != null)
        {
            if (adjacent.occupied)
            {
                //合併
                break;
            }

            newCell = adjacent;
            adjacent = tileGrid.GetAdjacentCell(adjacent, direction);
        }

        if (newCell != null)
        {
            tile.MoveTo(newCell);
        }

    }
}
