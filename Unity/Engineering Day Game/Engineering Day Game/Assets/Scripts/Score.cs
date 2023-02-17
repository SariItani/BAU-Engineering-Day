using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Score : MonoBehaviour
{
    public Text score, major;
    public int levelScore;
    string role;
    int number;

    void Start()
    {
        number = 0;
        major.text = PlayerPrefs.GetString("Major", "Computer");
        score.text = number.ToString();
    }
    
    public void ScorePoint(int points)
    {
        number += points;
        score.text = number.ToString();
        if (Convert.ToInt32(score.text) >= Convert.ToInt32(PlayerPrefs.GetString("Highscore", "0")))
        {
            PlayerPrefs.SetString("Highscore", score.text);
        }
        if (number >= levelScore)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public string ShowText()
    {
        return score.text;
    }
}
