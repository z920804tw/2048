using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileBoard tileBoard;
    public GameObject gameOverUI;

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
        gameOverUI.SetActive(false);
        gameOverUI.GetComponent<CanvasGroup>().alpha=0;

        tileBoard.ClearBoard();
        tileBoard.SpawnTile();
        tileBoard.SpawnTile();
        tileBoard.canMove = true;
    }
    public void GameOver()
    {
        tileBoard.canMove = false;
        StartCoroutine(ShowGameOver(0.5f));
    }


    IEnumerator ShowGameOver(float delay)
    {

        yield return new WaitForSeconds(delay);
        gameOverUI.SetActive(true);
        CanvasGroup canvasGroup = gameOverUI.GetComponent<CanvasGroup>();
        float timer = 0;
        while (timer < 1f)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, timer / 1f);
            timer += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;

    }
}
