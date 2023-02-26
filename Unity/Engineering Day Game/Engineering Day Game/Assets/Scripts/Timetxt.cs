using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timetxt : MonoBehaviour
{
    public Text timetxt;
    int duration;
    GameOver gameover;

    // Start is called before the first frame update
    void Start()
    {
        timetxt = gameObject.GetComponent<Text>();
        gameover = GameObject.Find("Manager").GetComponent<GameOver>();
    }

    // Update is called once per frame
    void Update()
    {
        // float someFloat = 42.7f;
        // int someInt = (int)Math.Round(someFloat);   // 43
        duration = (int)gameover.ShowTimer();
        timetxt.text = duration.ToString();
    }
}
