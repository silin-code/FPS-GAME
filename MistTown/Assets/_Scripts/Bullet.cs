using UnityEngine;

// Bullet: 子弹碰到东西就消失，3秒没碰到也自动销毁
public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f; // 子弹存活时间（秒）

    void Start()
    {
        // 3 秒后自动销毁，防止子弹永远飞在天上
        Destroy(gameObject, lifeTime);
    }

    // 碰到任何碰撞体时触发
    void OnCollisionEnter()
    {
        // 碰到东西立刻销毁自己
        Destroy(gameObject);
    }
}
