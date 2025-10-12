using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject enemy;
    [SerializeField] private float timeToSpawnEnemy;
    [SerializeField] private Vector2 minEnSpawnPos;
    [SerializeField] private Vector2 maxEnSpawnPos;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI enCountText;
    [SerializeField] private TextMeshProUGUI enDestroyedText;
    [SerializeField] private GameObject exitConfirmMenu;
    private float originalTimeToSpawnEnemy;
    private int enemiesCounter = 0;
    private int destroyedCounter = 0;
    public bool gamePaused = false;

    private void Start()
    {
        originalTimeToSpawnEnemy = timeToSpawnEnemy;
    }

    private void OnEnable()
    {
        InputManager.Instance.actions.UI.Exit.performed += ExitGame;
    }

    private void OnDisable()
    {
        InputManager.Instance.actions.UI.Exit.performed -= ExitGame;
    }

    private void Update()
    {
        SpawnEnemy();
        UpdateUI();
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
            enemiesCounter++;
        }
        else if (timeToSpawnEnemy > 0)
        {
            timeToSpawnEnemy -= Time.deltaTime;
        }
    }

    private void UpdateUI()
    {
        // Enemies counts
        enCountText.text = $"Enemies: {enemiesCounter}";
        enDestroyedText.text = $"Enemies destroyed: {destroyedCounter}";
    }

    private void ExitGame(InputAction.CallbackContext ctx)
    {
        exitConfirmMenu.SetActive(true);
        gamePaused = true;
        Time.timeScale = 0;
    }

    public void DestroyCounterAdd()
    {
        destroyedCounter++;
        enemiesCounter--;
    }
}
