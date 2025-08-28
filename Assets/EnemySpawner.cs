using UnityEngine;
using UnityEngine.Events; 
public class EnemySpawner : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [Header("Atrributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 1.2f;
    private int currentWave = 0;
    private float timeSinceLastSpawn;
    [SerializeField]private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }
    private void Start()
    {
        // Gọi wave đầu tiên khi game bắt đầu
        StartNextWave();
    }
    void Update()
    {
        if (!isSpawning)
            return;

        // Nếu vẫn còn enemy để spawn
        if (enemiesLeftToSpawn > 0)
        {
            timeSinceLastSpawn += Time.deltaTime;

            // Spawn theo tốc độ enemiesPerSecond
            if (timeSinceLastSpawn >= 1f / enemiesPerSecond)
            {
                SpawnEnemy();
                enemiesLeftToSpawn--;
                timeSinceLastSpawn = 0f;
            }
        }
        // Nếu không còn enemy để spawn + không còn enemy nào sống
        else if (enemiesAlive == 0)
        {
            isSpawning = false;
            Invoke(nameof(StartNextWave), timeBetweenWaves); // Đợi 1 khoảng trước khi bắt đầu wave mới
        }
    }
    private void EnemyDestroyed()
    {
         enemiesAlive--;
    }
    private void SpawnEnemy()
    {
        // Random chọn prefab enemy
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject enemy = Instantiate(prefab,LevelManager.main.StartPoint.position , Quaternion.identity);

        enemiesAlive++;

        // Khi enemy chết thì giảm số enemyAlive
        // Enemy enemyScript = enemy.GetComponent<Enemy>();
        // if (enemyScript != null)
        // {
        //     enemyScript.OnDeath += EnemyDied;
        // }
    }

    // private void EnemyDied()
    // {
    //     enemiesAlive--;
    // }

    private void StartNextWave()
    {
        currentWave++;

        // Số enemy sẽ tăng theo số mũ
        enemiesLeftToSpawn = Mathf.RoundToInt(baseEnemies * Mathf.Pow(difficultyScalingFactor, currentWave - 1));

        isSpawning = true;
        timeSinceLastSpawn = 0f;

        Debug.Log($"Wave {currentWave} bắt đầu! Sẽ spawn {enemiesLeftToSpawn} enemy.");
    }

    // Gọi hàm này khi bắt đầu game
   
}
