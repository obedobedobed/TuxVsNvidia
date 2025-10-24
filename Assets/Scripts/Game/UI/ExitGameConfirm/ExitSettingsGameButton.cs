using UnityEngine;

public class ExitSettingsGameButton : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenu;

    public void OnClick()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
