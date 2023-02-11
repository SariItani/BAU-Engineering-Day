using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void EndGame()
    {
        Debug.Log("Game Over.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
