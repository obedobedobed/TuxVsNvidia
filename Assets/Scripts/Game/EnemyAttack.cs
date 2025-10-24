using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyController enemy;
    private PlayerController player;
    private bool canAttack = false;

    private void Start()
    {
        GameObject playerTmp = GameObject.FindGameObjectWithTag("Player");
        if(playerTmp != null) player = playerTmp.GetComponent<PlayerController>();
        enemy = GetComponentInParent<EnemyController>();
    }

    private void Update()
    {
        if (canAttack && player != null)
        {
            if(enemy.attackCooldown >= enemy.originalAttackCooldown)
            {
                player.TakeDamage(enemy.damage);
                enemy.isAngry = true;
                enemy.attackCooldown = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canAttack = true;
            enemy.inAttackRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canAttack = false;
            enemy.inAttackRadius = false;
        }
    }
}
