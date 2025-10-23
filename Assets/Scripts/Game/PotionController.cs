using UnityEngine;
using UnityEngine.InputSystem;

public class PotionController : MonoBehaviour
{
    [SerializeField] private PotionType potionType;
    private int heal = 3;
    private float speedUp = 1.5f;
    private PlayerController player;
    private bool playerCanUse = false;

    private void OnEnable()
    {
        InputManager.Instance.actions.Player.Interact.performed += Heal;
    }

    private void OnDisable()
    {
        InputManager.Instance.actions.Player.Interact.performed -= Heal;
    }

    private void Start()
    {
        // Getting player controller
        GameObject playerTmp = GameObject.FindGameObjectWithTag("Player");
        if (playerTmp != null) player = playerTmp.GetComponent<PlayerController>();
    }

    private void Heal(InputAction.CallbackContext ctx)
    {
        // Checking for player in potion zone and calling method
        if (playerCanUse)
        {
            switch (potionType)
            {
                case PotionType.Heal:
                    bool healed = player.Heal(heal);
                    if (healed) Destroy(gameObject);
                    break;
                case PotionType.SpeedUp:
                    bool speedUpped = player.SpeedUp(speedUp);
                    if (speedUpped) Destroy(gameObject);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) playerCanUse = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) playerCanUse = false;
    }

    private enum PotionType
    {
        Heal, SpeedUp
    }
}
