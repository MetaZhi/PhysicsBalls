using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMesh ScoreText;

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

    }

    public void AddScore(int value)
    {
        score += value;
        ScoreText.text = score.ToString();
    }
}
