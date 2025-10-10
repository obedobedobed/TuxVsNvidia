using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float timeToSpawnEnemy;
    [SerializeField] private Vector2 minEnSpawnPos;
    [SerializeField] private Vector2 maxEnSpawnPos;
    private float originalTimeToSpawnEnemy;

    private void Start()
    {
        originalTimeToSpawnEnemy = timeToSpawnEnemy;
    }

    private void OnEnable()
    {
        InputManager.Instance.actions.UI.Quit.performed += QuitGame;
    }

    private void OnDisable()
    {
        InputManager.Instance.actions.UI.Quit.performed -= QuitGame;
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (timeToSpawnEnemy <= 0)
        {
            float spawnX = Random.Range(minEnSpawnPos.x, maxEnSpawnPos.x);
            float spawnY = Random.Range(minEnSpawnPos.y, maxEnSpawnPos.y);

            Vector2 enemySpawnPos = new Vector2(spawnX, spawnY);

            Instantiate(enemy, enemySpawnPos, Quaternion.identity);

            timeToSpawnEnemy = originalTimeToSpawnEnemy;
        }
        else if (timeToSpawnEnemy > 0)
        {
            timeToSpawnEnemy -= Time.deltaTime;
        }
    }

    private void QuitGame(InputAction.CallbackContext ctx)
    {
        Application.Quit();
    }
}
