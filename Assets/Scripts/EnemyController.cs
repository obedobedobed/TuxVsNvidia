using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Vector2 healthRange;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 speedRange;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = (int)Random.Range(healthRange.x, healthRange.y);
        speed = Random.Range(speedRange.x, speedRange.y);
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            Destroy(collision.gameObject);
        }
    }
}
