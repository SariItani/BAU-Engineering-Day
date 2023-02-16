using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetHighScore : MonoBehaviour
{
    TMP_Text text;
    private string score;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = PlayerPrefs.GetString("Highscore", "0");
    }
}
