using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCell : MonoBehaviour
{
    public Vector2Int coordinates;
    public Tile tile;

    public bool empty{
        get{return tile==null;}
    }
    public bool occupied{
        get{return tile!=null;}
    }

}
