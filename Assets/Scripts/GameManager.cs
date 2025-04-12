using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool _isGameOver = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)&&_isGameOver)
        {
            //restart Level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
