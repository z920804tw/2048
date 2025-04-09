using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRow : MonoBehaviour
{
    public TileCell[] cells;
    public int rowNum;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        //取的該Row底下的所有Cell
        cells = GetComponentsInChildren<TileCell>();

        //初始化座標
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].coordinates=new Vector2Int(i,rowNum);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
