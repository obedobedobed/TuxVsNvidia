using UnityEngine;

public class ContinueGameButton : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void OnClick(GameObject pauseMenu)
    {
        gameController.gamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
