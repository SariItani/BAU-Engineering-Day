using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public float timer;
    public float levelTime;

    void Update()
    {
        timer += Time.deltaTime;
        if (levelTime > 0)
        {
            if (timer > levelTime)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void EndGame()
    {
        Debug.Log("Game Over.");
        SceneManager.LoadScene(4);
    }

    public void WinGame()
    {
        Debug.Log("Next Level running...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
