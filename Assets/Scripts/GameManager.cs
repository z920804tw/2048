using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("參考物件設定")]
    public TileBoard tileBoard;
    public GameObject gameOverUI;
    public GameObject inPlayUI;
    public GameObject exitUI;

    [Header("分數文字")]
    public TMP_Text scoreText;
    public TMP_Text heighestScoreText;
    int score;
    bool isStop;

    // Start is called before the first frame update
    void Start()
    {
        isStop = false;
        heighestScoreText.text = $"{LoadHeighestScore()}";
        // StarNewGame();
    }
    public void StopTime()
    {
        isStop=!isStop;
        if (isStop)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale=1;
        }
    }

    public void StarNewGame()
    {
        score = 0;
        scoreText.text = $"{score}";

        gameOverUI.SetActive(false);
        gameOverUI.GetComponent<CanvasGroup>().alpha = 0;

        tileBoard.ClearBoard();
        tileBoard.SpawnTile();
        tileBoard.SpawnTile();

        inPlayUI.SetActive(true);

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

    public void IncreaseScore(int score)
    {
        this.score += score;
        scoreText.text = $"{this.score}";

        int heighest = LoadHeighestScore();
        if (this.score > heighest)
        {
            PlayerPrefs.SetInt("HeightesScore", this.score);
            heighestScoreText.text = $"{this.score}";
        }
    }
    public int LoadHeighestScore()
    {
        return PlayerPrefs.GetInt("HeightesScore", 0);

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
