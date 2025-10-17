using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Range healthRange;
    [SerializeField] private Range damageRange;
    [SerializeField] private Range speedRange;
    [SerializeField] public float attackCooldown;
    public float originalAttackCooldown { get; private set; }
    private GameObject player;
    private GameController gameController;
    private Animator anim;

    public bool isAngry = false;
    public bool inAttackRadius;
    public float notAttacked = 0;

    private int health;
    public int damage { get; private set; }
    private float speed;

    private bool dead = false;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        anim = GetComponent<Animator>();
        originalAttackCooldown = attackCooldown;
        player = GameObject.FindGameObjectWithTag("Player");
        health = (int)Random.Range(healthRange.MinimalValue, healthRange.MaximalValue);
        damage = (int)Random.Range(damageRange.MinimalValue, damageRange.MaximalValue);
        speed = Random.Range(speedRange.MinimalValue, speedRange.MaximalValue);
    }

    private void Update()
    {
        if (health <= 0 && !dead)
        {
            gameController.DestroyCounterAdd();
            anim.SetTrigger("Death");
            Invoke(nameof(SelfDestroy), 0.5f);
            damage = 0;
            dead = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (attackCooldown < originalAttackCooldown)
        {
            attackCooldown += Time.deltaTime;
        }

        // Animations
        notAttacked += Time.deltaTime;

        if (isAngry)
        {
            anim.SetBool("IsAngry", true);
        }
        else if (notAttacked > 0.4f)
        {
            anim.SetBool("IsAngry", false);
            isAngry = false;
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
