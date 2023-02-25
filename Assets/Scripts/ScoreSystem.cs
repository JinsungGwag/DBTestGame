using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    private static ScoreSystem _instance;
    public static ScoreSystem Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            else
                return null;
        }
        set
        {
            _instance = value;
        }
    }

    [SerializeField]
    private Text _scoreText;

    private static int _score;

    private void Awake()
    {
        Instance = this;
 
        _score = 0;
        _scoreText.text = "Score: " + _score;
    }

    public void RaiseScore()
    {
        ++_score;
        _scoreText.text = "Score: " + _score;
    }

    public static int GetScore()
    {
        return _score;
    }
}
