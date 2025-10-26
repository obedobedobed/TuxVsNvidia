using UnityEngine;
using UnityEngine.InputSystem;

public class PotionController : MonoBehaviour
{
    [SerializeField] private PotionType potionType;
    [SerializeField] private bool inShop;
    private int heal = 3;
    private float speedUp = 1.5f;
    private PlayerController player;
    private bool playerCanUse = false;

    private void OnEnable()
    {
        InputManager.Instance.actions.Player.Interact.performed += Use;
    }

    private void OnDisable()
    {
        InputManager.Instance.actions.Player.Interact.performed -= Use;
    }

    private void Start()
    {
        // Getting player controller
        GameObject playerTmp = GameObject.FindGameObjectWithTag("Player");
        if (playerTmp != null) player = playerTmp.GetComponent<PlayerController>();
    }

    private void Use(InputAction.CallbackContext ctx)
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
                case PotionType.Pacman:
                    bool pacmaned = player.SpeedUp(speedUp * 2);
                    if (pacmaned) Destroy(gameObject);
                    break;
                case PotionType.Shield:
                    bool takedShield = player.TakeShield();
                    if (takedShield) Destroy(gameObject);
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

}
public enum PotionType
{
    Heal, SpeedUp, Shield, Pacman
}
