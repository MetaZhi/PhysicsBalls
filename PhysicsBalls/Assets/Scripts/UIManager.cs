using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject Panel;
    public Text ScoreText;

    public static UIManager Instance { get; private set; }
    void Start()
    {
        Instance = this;
    }

    public void ShowPanel(int score)
    {
        Panel.SetActive(true);
        ScoreText.text = score.ToString();
    }

    public void OnRestartGame()
    {
        Panel.SetActive(false);
        GameManager.Instance.ResetGame();
    }
}
