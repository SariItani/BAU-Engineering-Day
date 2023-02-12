using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public Text score;
    public int levelScore;
    int number;

    void Start()
    {
        number = 0;
        score.text = number.ToString();
    }
    
    public void ScorePoint()
    {
        number++;
        score.text = number.ToString();
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
