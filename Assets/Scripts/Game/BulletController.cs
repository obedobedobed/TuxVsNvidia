using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    private Rigidbody2D rb;

    private void Start()
    {
        // Getting components
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Moving
        rb.linearVelocity = transform.right * speed;
        lifeTime -= Time.fixedDeltaTime;

        // Destroying if life time ended
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checking for collision with enemy
        if (!collision.gameObject.CompareTag("Bullet") && !collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
