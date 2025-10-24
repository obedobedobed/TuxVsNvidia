using UnityEngine;

public class SettingsGameButton : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenu;

    public void OnClick()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
