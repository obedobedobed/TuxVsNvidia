using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionText;

    private void Start()
    {
        versionText.text = $"v{Application.version}";
    }
}
