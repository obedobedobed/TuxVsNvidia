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
    [SerializeField] private TextMeshProUGUI waveCount;
    [SerializeField] private TextMeshProUGUI waveTime;

    [Header("Pause UI")]
    [SerializeField] private GameObject exitConfirmMenu;

    [Header("Game over UI")]
    [SerializeField] private TextMeshProUGUI enDestroyedTextGO;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject gameOverMenu;

    [Header("Waves")]
    [SerializeField] private int timeToNewWave;
    [SerializeField] private int timeToWaveEnd;
    public int getTimeToNewWave
    {
        get
        {
            return timeToNewWave;
        }
    }
    private bool waveIsGoing = false;
    private int currentWave = 1;
    public Timer timeToNewWaveTimer { get; private set; } = new Timer();
    private Timer timeToWaveEndTimer = new Timer();
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
        // Time counting
        timeSurvived.UpdateTime(Time.deltaTime);

        // Calling methods
        UpdateWaves();
        if (waveIsGoing) SpawnEnemy();
        UpdateUI();
    }

    private void SpawnEnemy()
    {
        if (timeToSpawnEnemy <= 0)
        {
            // Getting random position to spawn
            float spawnX = Random.Range(minEnSpawnPos.x, maxEnSpawnPos.x);
            float spawnY = Random.Range(minEnSpawnPos.y, maxEnSpawnPos.y);
            Vector2 enemySpawnPos = new Vector2(spawnX, spawnY);

            // Spawning
            Instantiate(enemy, enemySpawnPos, Quaternion.identity);

            // Cooldown
            timeToSpawnEnemy = originalTimeToSpawnEnemy;

            // Counting
            enemiesCounter++;

            // Modifying spawn cooldown
            timeToSpawnEnemy /= enemySpawnMod;
            originalTimeToSpawnEnemy /= enemySpawnMod;
        }
        else if (timeToSpawnEnemy > 0)
        {
            // Cooldown
            timeToSpawnEnemy -= Time.deltaTime;
        }
    }

    private void UpdateUI()
    {
        // Enemies counts
        enCountText.text = $"Enemies: {enemiesCounter}";
        enDestroyedTextUI.text = $"Enemies destroyed: {destroyedCounter}";

    }

    private void UpdateWaves()
    {
        if (!waveIsGoing)
        {
            timeToNewWaveTimer.UpdateTime(Time.deltaTime);

            waveCount.text = $"Wave {currentWave}";
            waveTime.text = $"Time to new wave: {(int)(timeToNewWave - timeToNewWaveTimer.totalSeconds + 1)}";

            enemiesCounter = 0;

            if (timeToNewWaveTimer.totalSeconds >= timeToNewWave)
            {
                waveIsGoing = true;
                timeToNewWaveTimer.ResetTime();
            }
        }
        else if (waveIsGoing)
        {
            timeToWaveEndTimer.UpdateTime(Time.deltaTime);

            waveTime.text = $"Time to wave end: {(int)(timeToWaveEnd - timeToWaveEndTimer.totalSeconds + 1)}";

            if (timeToWaveEndTimer.totalSeconds >= timeToWaveEnd)
            {
                waveIsGoing = false;
                timeToWaveEndTimer.ResetTime();

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject[] potions = GameObject.FindGameObjectsWithTag("Potion");

                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<EnemyController>().death = true;
                    enemies[i].GetComponent<EnemyController>().autoDeath = true;
                }

                for (int i = 0; i < potions.Length; i++)
                {
                    potions[i].GetComponent<SelfDestroyer>().SelfDestroy(animate: true);
                }

                currentWave++;
            }
        }
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
