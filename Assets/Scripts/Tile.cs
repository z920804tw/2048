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

    public int number = 2;

    public bool lockTile;
    // Start is called before the first frame update

    public void Start()
    {
        background = GetComponent<Image>();
        numberText = GetComponentInChildren<TMP_Text>();
    }
    public void SetTileState(TileState state) //設定狀態
    {
        tileState = state;


        background.color = tileState.backgroundColor;
        numberText.color = tileState.textColor;
        numberText.text = $"{number}";

    }

    public void SetTilePos(TileCell tileCell) //設定位置
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

        StartCoroutine(TileMoveAnim(cell.transform.position, false));
    }

    public void MergeCell(TileCell tileCell)
    {
        if (cell != null) //先判斷有沒有本身有沒有紀錄cell了，如果有就清空他的tile與cell資料
        {
            cell.tile = null;
        }
        cell = null;
        tileCell.tile.lockTile = true;
        StartCoroutine(TileMoveAnim(tileCell.transform.position, true)); //A方塊會移動到B方塊上
    }


    IEnumerator TileMoveAnim(Vector2 targetPos, bool merge) //方塊移動的動畫
    {
        float timer = 0;
        float duration = 0.2f;

        Vector2 startPos = transform.position;

        while (timer < duration)
        {

            transform.position = Vector2.Lerp(startPos, targetPos, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos; //固定到目標位置


        if (merge)
        {
            Destroy(gameObject); //將被合併的物件刪掉
        }

    }
}
