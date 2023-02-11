using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Text score;
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
    }

    public string ShowText()
    {
        return score.text;
    }
}
