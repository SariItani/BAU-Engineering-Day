using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void EndGame()
    {
        Debug.Log("Game Over.");
        SceneManager.LoadScene(3);
    }
}
