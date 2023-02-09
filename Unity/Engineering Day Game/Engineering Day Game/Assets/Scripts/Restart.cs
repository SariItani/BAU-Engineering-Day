using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("Game Restarting...");
        SceneManager.LoadScene(0);
    }
}
