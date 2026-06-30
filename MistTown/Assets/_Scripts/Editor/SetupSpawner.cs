using UnityEngine;
using UnityEditor;

public class SetupSpawner : MonoBehaviour
{
    [MenuItem("迷雾镇/一键设置敌人刷新器")]
    static void Install()
    {
        // 1. 删除场景里旧的 Enemy（如果有）
        GameObject oldEnemy = GameObject.Find("Enemy");
        if (oldEnemy != null) Object.DestroyImmediate(oldEnemy);

        // 2. 创建敌人预制体（从 Capsule）
        GameObject enemyObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        enemyObj.name = "Enemy";
        enemyObj.transform.position = Vector3.zero;

        // 添加需要的脚本
        enemyObj.AddComponent<Enemy>();
        EnemyHealth health = enemyObj.AddComponent<EnemyHealth>();
        health.maxHealth = 30f;

        // 存成预制体
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(enemyObj, "Assets/_Prefabs/Enemy.prefab");
        Object.DestroyImmediate(enemyObj);

        // 3. 找或创建 EnemySpawner
        GameObject spawner = GameObject.Find("EnemySpawner");
        if (spawner == null)
            spawner = new GameObject("EnemySpawner");

        EnemySpawner es = spawner.GetComponent<EnemySpawner>();
        if (es == null) es = spawner.AddComponent<EnemySpawner>();
        es.enemyPrefab = prefab;
        es.maxEnemies = 3;
        es.spawnRadius = 8f;
        es.spawnDelay = 3f;

        Debug.Log("✅ 设置完成！▶ Play 后敌人会在玩家周围刷新");
    }
}
