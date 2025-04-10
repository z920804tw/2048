using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileState tileState;
    public TileCell cell;

    public Image background;
    public TMP_Text numberText;

    public int number;
    // Start is called before the first frame update

    public void Start()
    {
        background = GetComponent<Image>();
        numberText = GetComponentInChildren<TMP_Text>();
    }
    public void SetTileState(TileState state)
    {
        tileState = state;
        number = tileState.number;

        background.color = tileState.backgroundColor;
        numberText.color = tileState.textColor;
        numberText.text = $"{number}";

    }

    public void SetTilePos(TileCell tileCell)
    {
        cell = tileCell; //先設定好cell=傳入的cell
        cell.tile = this;  //在設定該cell的tile為自己

        transform.position = cell.transform.position;
    }

    public void MoveTo(TileCell tileCell) //移動
    {
        if (cell != null) //先判斷有沒有本身有沒有紀錄cell了，如果有就清空他的tile
        {
            cell.tile = null;
        }
        cell = tileCell;            //設定cell為新的cell，並且該cell的tile也設定成自己
        cell.tile = this;        

        transform.position = cell.transform.position;
    }
}
