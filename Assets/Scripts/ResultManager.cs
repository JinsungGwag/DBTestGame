using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private InputField _nameInput;
    [SerializeField]
    private GameObject _enterPanel;

    [SerializeField]
    private Text _nameResultText;
    [SerializeField]
    private Text _scoreResultText;

    private int _gameScore;

    private string _secretKey = "mySecretKey";
    private string _addScoreURL = "http://localhost/HighScoreGame/addscore.php?";
    private string _highScoreURL = "http://localhost/HighScoreGame/display.php";

    private void Start()
    {
        _gameScore = ScoreSystem.GetScore();
        _scoreText.text = "Score: " + _gameScore;
    }

    public void SendScore()
    {
        StartCoroutine(PostScore());
        _enterPanel.SetActive(false);
    }

    public void LoadInGame()
    {
        SceneManager.LoadScene("InGame");
    }

    private void GetScore()
    {
        _nameResultText.text = "Player: \n \n";
        _scoreResultText.text = "Score: \n \n";
        StartCoroutine(GetAllScores());
    }

    private IEnumerator PostScore()
    {
        string hash = HashInput(_nameInput.text + _gameScore + _secretKey);
        string post_url = _addScoreURL + "name=" + UnityWebRequest.EscapeURL(_nameInput.text) + "&score=" + _gameScore + "&hash=" + hash;
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);

        yield return hs_post.SendWebRequest();
        GetScore();
    }

    private string HashInput(string input)
    {
        SHA256Managed hm = new SHA256Managed();
        byte[] hashValue = hm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
        string hash_convert = BitConverter.ToString(hashValue).Replace("-", "").ToLower();

        return hash_convert;
    }

    private IEnumerator GetAllScores()
    {
        UnityWebRequest hs_get = UnityWebRequest.Get(_highScoreURL);
        yield return hs_get.SendWebRequest();

        string dataText = hs_get.downloadHandler.text;
        MatchCollection mc = Regex.Matches(dataText, @"_");
        if(mc.Count > 0)
        {
            string[] splitData = Regex.Split(dataText, @"_");
            for(int i = 0; i < mc.Count; i++)
            {
                if (i % 2 == 0)
                    _nameResultText.text += splitData[i];
                else
                    _scoreResultText.text += splitData[i];
            }
        }
    }
}
