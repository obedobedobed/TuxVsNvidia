using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private AudioSource audStep;
    [SerializeField] private Range audStepPitchRange;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private AudioSource audDrink;
    [SerializeField] private GameObject shield;
    private int maxHealth;
    private bool isRuning = false;
    private bool underShield = false;
    private Vector2 direction;
    private Rigidbody2D rb;
    private Animator anim;
    private GameController gameController;
    private ShotgunController shotgun;

    private void Start()
    {
        // Getting components
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        shotgun = GetComponentInChildren<ShotgunController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Sigma mode
        if (GlobalVariables.SigmaMode)
        {
            speed *= 1.2f;
            health *= 5;
        }

        // Getting max health
        maxHealth = health;
    }

    private void Update()
    {
        // Death logic
        if (health <= 0)
        {
            gameController.EndGame();
            Destroy(gameObject);
        }

        // UI stats updating
        healthText.text = $"Health: {health}/{maxHealth}";

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
            AudioManager.PlaySFX(audStep);
        }
        else if (direction == new Vector2(0, 0) && isRuning)
        {
            isRuning = false;
            AudioManager.PlaySFX(audStep, stop: true);
        }
    }

    public void TakeDamage(int damage)
    {
        // For no health under zero
        if (underShield) return;
        if (health - damage > 0) health -= damage;
        else if (health - damage <= 0) health = 0;
    }

    // Methods for potions
    public bool Heal(int heal)
    {
        // Checking for health is not max
        if (health != maxHealth)
        {
            AudioManager.PlaySFX(audDrink);

            // Checking for no regened health is over than maximal
            if (health + heal < maxHealth)
            {
                health += heal;
                return true;
            }
            else
            {
                health = maxHealth;
                return true;
            }
        }
        else return false;
    }

    public bool SpeedUp(float speedUp)
    {
        AudioManager.PlaySFX(audDrink);

        // Moding speed by speed modifier
        speed *= speedUp;

        // Waiting for time and taking effect back
        StartCoroutine(SpeedDown(speedUp));
        
        return true;
    }

    private IEnumerator SpeedDown(float speedDown)
    {
        yield return new WaitForSeconds(5);
        speed /= speedDown;
    }

    public bool TakeShield()
    {
        if (!underShield)
        {
            AudioManager.PlaySFX(audDrink);

            // Activating shield
            shield.SetActive(true);
            underShield = true;

            // Waiting for time and taking effect back
            StartCoroutine(LoseShield());
            return true;
        }
        else return false;

    }

    private IEnumerator LoseShield()
    {
        yield return new WaitForSeconds(10);
        shield.SetActive(false);
        underShield = false;
    }
}