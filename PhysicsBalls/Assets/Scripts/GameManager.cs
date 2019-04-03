using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMesh ScoreText;

    public bool canShoot = true;

    int score;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        ScoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnGameOver();
        }
    }

    public void AddScore(int value)
    {
        score += value;
        ScoreText.text = score.ToString();
    }

    public void OnGameOver()
    {
        UIManager.Instance.ShowPanel(score);
        canShoot = false;
    }

    public void ResetGame()
    {
        // 重置小球的数量
        BallManager.Instance.ResetBalls();

        // 重置砖块
        BlockManager.Instance.ResetBlocks();

        // 重置分数
        score = 0;
        ScoreText.text = score.ToString();

        Invoke("SetCanShoot", 0.1f);
    }

    void SetCanShoot()
    {
        canShoot = true;
    }
}
