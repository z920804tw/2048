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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
