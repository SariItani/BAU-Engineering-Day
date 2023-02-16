using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void EndGame()
    {
        Debug.Log("Game Over.");
        SceneManager.LoadScene(4);
    }

    public void WinGame()
    {
        Debug.Log("YOU WIN!!!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
