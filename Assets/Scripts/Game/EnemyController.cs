using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Range healthRange;
    [SerializeField] private Range damageRange;
    [SerializeField] private Range speedRange;
    [SerializeField] public float attackCooldown;
    [SerializeField] private float angrySpeedMod;
    [SerializeField] private GameObject[] potions;
    [SerializeField] private int potionSpawnChance;
    public float originalAttackCooldown { get; private set; }
    private GameObject player;
    private GameController gameController;
    private Animator anim;

    [HideInInspector] public bool isAngry = false;
    [HideInInspector] public bool inAttackRadius;

    private int health;
    public int damage { get; private set; }
    private float speed;

    [HideInInspector] public bool death = false;
    private bool dead;
    [HideInInspector] public bool autoDeath = false;
    private bool speedModed = false;

    private void Start()
    {
        // Getting components
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        anim = GetComponent<Animator>();
        originalAttackCooldown = attackCooldown;
        player = GameObject.FindGameObjectWithTag("Player");
        // Getting values from ranges
        health = (int)Random.Range(healthRange.MinimalValue, healthRange.MaximalValue);
        damage = (int)Random.Range(damageRange.MinimalValue, damageRange.MaximalValue);
        speed = Random.Range(speedRange.MinimalValue, speedRange.MaximalValue);
    }

    private void Update()
    {
        // Angry mode
        if (isAngry && !speedModed)
        {
            speed *= angrySpeedMod;
            speedModed = true;
        }

        // Death
        if (health <= 0 && !dead) death = true;

        if (death)
        {
            gameController.DestroyCounterAdd();
            anim.SetTrigger("Death");
            damage = 0;
            if (Random.Range(0, 100) < potionSpawnChance && !autoDeath)
            {
                Instantiate(potions[Random.Range(0, potions.Length)], transform.position, Quaternion.identity);
            }
            death = false;
            dead = true;
            Invoke(nameof(SelfDestroy), 0.5f);
        }

        // Moving
        if(player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        // Cooldown
        if (attackCooldown < originalAttackCooldown)
        {
            attackCooldown += Time.deltaTime;
        }

        // Animations
        if (isAngry)
        {
            anim.SetBool("IsAngry", true);
        }

        if (inAttackRadius)
        {
            anim.SetBool("IsRun", false);
        }
        else if (!inAttackRadius)
        {
            anim.SetBool("IsRun", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Getting damage
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            Destroy(collision.gameObject);
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}