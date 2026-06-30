using UnityEngine;

// EnemySpawner: 敌人在玩家周围随机位置重生，无限刷怪
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;    // 敌人预制体
    public float spawnRadius = 8f;    // 以玩家为中心的出生范围
    public float spawnDelay = 3f;     // 死亡后几秒重生
    public int maxEnemies = 3;        // 同时存在的最大敌人数

    private int currentAlive = 0;
    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
            player = playerObj.transform;

        // 开局生成第一批敌人
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        // 在玩家周围随机位置生成敌人
        Vector3 randomPos = player.position + Random.insideUnitSphere * spawnRadius;
        randomPos.y = 0.5f; // 让敌人站在地面上

        GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);

        // 监听敌人死亡事件
        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        if (health != null)
        {
            currentAlive++;
            EnemyHealth aliveHealth = health;
            health.onDeath += () => OnEnemyDied(aliveHealth);
        }
    }

    void OnEnemyDied(EnemyHealth enemyHealth)
    {
        currentAlive--;

        // 延迟重生
        Invoke(nameof(SpawnEnemy), spawnDelay);
    }
}
