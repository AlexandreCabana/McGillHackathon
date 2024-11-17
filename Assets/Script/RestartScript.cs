using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        Time.timeScale = 1; // Resume game speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}