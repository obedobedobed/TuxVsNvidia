using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public InputSystem_Actions actions;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        if (Instance != null) return;

        GameObject go = new GameObject("InputManager");
        Instance = go.AddComponent<InputManager>();
        DontDestroyOnLoad(go);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        actions = new InputSystem_Actions();
        actions.Enable();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            actions.Disable();
            actions = null;
            Instance = null;
        }
    }

    public static void ChangeInputMaps(InputType inputType, bool enabling)
    {
        switch (inputType)
        {
            case InputType.Player:
                if (enabling) Instance.actions.Player.Enable();
                else Instance.actions.Player.Disable();
                break;
            case InputType.UI:
                if (enabling) Instance.actions.UI.Enable();
                else Instance.actions.UI.Disable();
                break;
        }
    }

    public enum InputType
    {
        Player,
        UI
    }
}
