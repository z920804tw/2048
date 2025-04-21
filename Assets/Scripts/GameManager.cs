using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
    public TMP_Text heighestMergeText;
    public TMP_Text gameOverScoreText;
    public TMP_Text gameOverHScoreText;
    int score;
    bool isStop;

    // Start is called before the first frame update
    void Start()
    {
        isStop = false;
        heighestScoreText.text = $"{LoadHeighestScore()}";
        heighestMergeText.text = $"{LoadHeighestMerge()}";
        // StarNewGame();
    }
    public void StopTime()
    {
        isStop = !isStop;
        if (isStop)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
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
        gameOverScoreText.text = $"本回合分數:{score}";
        gameOverHScoreText.text = $"歷史最高分數:{LoadHeighestScore()}";
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
        int heighestMerge = LoadHeighestMerge();
        if (this.score > heighest)
        {
            PlayerPrefs.SetInt("HeightesScore", this.score);
            heighestScoreText.text = $"{LoadHeighestScore()}";
        }

        if (score > heighestMerge)
        {
            PlayerPrefs.SetInt("HeighestMerge",score);
            heighestMergeText.text=$"{LoadHeighestMerge()}";
        }
    }
    public int LoadHeighestScore()
    {
        return PlayerPrefs.GetInt("HeightesScore", 0);

    }
    public int LoadHeighestMerge()
    {
        return PlayerPrefs.GetInt("HeighestMerge", 0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
