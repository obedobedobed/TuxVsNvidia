using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGameButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
