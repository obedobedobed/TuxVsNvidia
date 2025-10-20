using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealController : MonoBehaviour
{
    private int heal = 3;
    private PlayerController player;
    private bool playerCanHeal = false;

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
        // Checking for player in heal zone and calling heal method
        if (playerCanHeal)
        {
            bool healed = player.Heal(heal);
            if (healed) Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) playerCanHeal = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) playerCanHeal = false;
    }
}
