using UnityEngine;

// Bullet: 子弹碰到东西就消失，3秒没碰到也自动销毁
public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;      // 子弹存活时间（秒）
    public float damageAmount = 10f; // 造成的伤害

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 检测被打中的物体是否有 EnemyHealth
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageAmount); // 扣敌人血
        }

        // 子弹碰到任何东西都销毁
        Destroy(gameObject);
    }
}
