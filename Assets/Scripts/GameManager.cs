using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileBoard tileBoard;

    // Start is called before the first frame update
    void Start()
    {
        StarNewGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StarNewGame()
    {
        tileBoard.ClearBoard();
        tileBoard.SpawnTile();
        tileBoard.SpawnTile();
        tileBoard.canMove = true;
    }
    public void GameOver()
    {
        tileBoard.canMove=false;
    }
}
