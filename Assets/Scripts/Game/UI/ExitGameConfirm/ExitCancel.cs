using UnityEngine;

public class ExitCancel : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    
    public void OnClick(GameObject quitConfirmMenu)
    {
        gameController.gamePaused = false;
        quitConfirmMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
