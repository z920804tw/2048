using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public int row;
    public int col;
    public GameObject gridParent;
    public GameObject cellPrefab;
    public GameObject rowPrefab;



    void Awake()
    {
        GenerationGrid();
    }
    void GenerationGrid()
    {
        List<GameObject> rowParent=new List<GameObject>();
        int num=0;
        //生成列
        for (int i = 0; i < row; i++)
        {
            GameObject row = Instantiate(rowPrefab);
            row.transform.SetParent(gridParent.transform);
            rowParent.Add(row);
            row.GetComponent<TileRow>().rowNum=num;
            num++;

            for (int j = 0; j < col; j++)
            {
                GameObject cell= Instantiate(cellPrefab);
                cell.transform.SetParent(rowParent[i].transform);
            }
        }
    }
}
