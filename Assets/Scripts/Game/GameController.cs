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
    [SerializeField] private float enemySpawnMod;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI enCountText;
    [SerializeField] private TextMeshProUGUI enDestroyedTextUI;

    [Header("Pause UI")]
    [SerializeField] private GameObject exitConfirmMenu;

    [Header("Game over UI")]
    [SerializeField] private TextMeshProUGUI enDestroyedTextGO;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject gameOverMenu;

    private float originalTimeToSpawnEnemy;
    private int enemiesCounter = 0;
    private int destroyedCounter = 0;
    [HideInInspector] public bool gamePaused = false;
    private Timer timeSurvived = new Timer();


    private void OnEnable()
    {
        InputManager.Instance.actions.UI.Exit.performed += ExitGame;
    }

    private void OnDisable()
    {
        InputManager.Instance.actions.UI.Exit.performed -= ExitGame;
    }

    private void Start()
    {
        originalTimeToSpawnEnemy = timeToSpawnEnemy;
    }

    private void Update()
    {
        timeSurvived.UpdateTime(Time.deltaTime);
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

            timeToSpawnEnemy /= enemySpawnMod;
            originalTimeToSpawnEnemy /= enemySpawnMod;
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
        enDestroyedTextUI.text = $"Enemies destroyed: {destroyedCounter}";
    }

    private void ExitGame(InputAction.CallbackContext ctx)
    {
        exitConfirmMenu.SetActive(true);
        gamePaused = true;
        Time.timeScale = 0;
    }

    public void DestroyCounterAdd()
    {
        // Adding destroyed enemies and removing it from counter
        destroyedCounter++;
        enemiesCounter--;
    }

    public void EndGame()
    {
        gameOverMenu.SetActive(true);
        gameOverMenu.GetComponent<Animator>().SetTrigger("Animate");
        timeText.text = $"Time: {timeSurvived.WriteTime()}";
        enDestroyedTextGO.text = $"Enemies destroyed: {destroyedCounter}";
        timeSurvived.ResetTime();
    }
}
