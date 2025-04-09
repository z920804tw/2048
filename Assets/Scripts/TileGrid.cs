using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows;
    public TileCell[] cells;

    public int size;
    [SerializeField] int height;
    [SerializeField] int weight;
    // Start is called before the first frame update

    void Awake()
    {

    }
    void Start()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();

        size = cells.Length;
        height = rows.Length;
        weight = size / height;
    }


}
