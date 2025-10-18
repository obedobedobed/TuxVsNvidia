using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private AudioSource audStep;
    [SerializeField] private Range audStepPitchRange;
    [SerializeField] private TextMeshProUGUI healthText;
    private bool isRuning = false;
    private Vector2 direction;
    private Rigidbody2D rb;
    private Animator anim;
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Sigma mode
        if (GlobalVariables.SigmaMode)
        {
            speed *= 1.2f;
            health *= 5;
        }
    }

    private void Update()
    {
        // Death logic
        if(health <= 0)
        {
            gameController.EndGame();
            Destroy(gameObject);
        }

        // UI stats updating
        healthText.text = $"Health: {health}";

        // Run logic
        direction = InputManager.Instance.actions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Run logic
        rb.linearVelocity = direction * speed;

        // Animations
        if (direction != new Vector2(0, 0))
        {
            anim.SetBool("IsRun", true);
        }
        else
        {
            anim.SetBool("IsRun", false);
        }

        // Sounds
        audStep.pitch = Random.Range(audStepPitchRange.MinimalValue, audStepPitchRange.MaximalValue);

        if (direction != new Vector2(0, 0) && !isRuning)
        {
            isRuning = true;
            AudioManager.PlaySFX(audStep, false);
        }
        else if (direction == new Vector2(0, 0) && isRuning)
        {
            isRuning = false;
            AudioManager.PlaySFX(audStep, true);
        }
    }

    public void TakeDamage(int damage)
    {
        // For no health under zero
        if (health - damage > 0)
        {
            health -= damage;
        }
        else if(health - damage <= 0)
        {
            health = 0;
        }
    }
}
