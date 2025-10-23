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
    private int maxHealth;
    private bool isRuning = false;
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
        if (health - damage > 0) health -= damage;
        else if (health - damage <= 0) health = 0;
    }

    public void TakeBuff(float buffTime, float healthMod, float speedMod, float bulletsMod)
    {
        StartCoroutine(TakeBuffCoroutine(buffTime, healthMod, speedMod, bulletsMod));
    }

    private IEnumerator TakeBuffCoroutine(float buffTime, float healthMod, float speedMod, float bulletsMod)
    {
        // Saving values
        int originalHealth = health;
        float originalSpeed = speed;
        int originalBulletsCount = shotgun.bulletsCount;

        // Taking the buff
        health = (int)(health * healthMod);
        speed *= speedMod;
        shotgun.bulletsCount = (int)(shotgun.bulletsCount * bulletsMod);

        // Waiting
        yield return new WaitForSeconds(buffTime);

        // Buff undo
        health = originalHealth;
        speed = originalSpeed;
        shotgun.bulletsCount = originalBulletsCount;
    }

    // Methods for potions
    public bool Heal(int heal)
    {
        if (health != maxHealth)
        {
            AudioManager.PlaySFX(audDrink);

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
        speed *= speedUp;
        StartCoroutine(SpeedDown(speedUp));
        
        return true;
    }

    private IEnumerator SpeedDown(float speedDown)
    {
        yield return new WaitForSeconds(5);
        speed /= speedDown;
    }
}