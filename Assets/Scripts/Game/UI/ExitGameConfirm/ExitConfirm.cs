using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitConfirm : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Menu");
    }
}
