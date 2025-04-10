
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileState tileState;
    public TileCell cell;
    public int number;

    [SerializeField] Image background;
    [SerializeField] TMP_Text numberText;
    // Start is called before the first frame update
    void Awake()
    {
        background = GetComponent<Image>();
        numberText = GetComponentInChildren<TMP_Text>();
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetState(TileState state)
    {
        tileState = state;
        number = tileState.number;

        background.color = tileState.backgroundColor;
        numberText.color = tileState.textColor;
        numberText.text = tileState.number.ToString();
    }
    public void SetTile(TileCell tileCell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        cell = tileCell;
        cell.tile = this;
        transform.position = cell.transform.position;
        
    }

    public void MoveTo(TileCell tileCell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        cell = tileCell;
        cell.tile = this;
        transform.position = cell.transform.position;
    }
}
