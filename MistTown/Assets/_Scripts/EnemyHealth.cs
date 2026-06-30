using UnityEngine;

// EnemyHealth: 敌人血量，被打到 0 就销毁
public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 30f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // 子弹调用这个方法造成伤害
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject); // 敌人死亡
        }
    }
}
