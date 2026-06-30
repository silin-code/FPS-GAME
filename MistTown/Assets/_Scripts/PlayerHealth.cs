using UnityEngine;

// PlayerHealth: 玩家血量管理，挂在 Player 身上
public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;            // 最大血量
    public float currentHealth { get; private set; } // 当前血量（UIManager 可读，外部不可写）

    void Start()
    {
        currentHealth = maxHealth; // 开局满血
    }

    // 受伤：外部调用（比如敌人撞到你时）
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        // 限制最低为 0，防止负数
        if (currentHealth < 0) currentHealth = 0;
        Debug.Log("受到 " + amount + " 点伤害，剩余血量: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 回血：血包调用
    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    void Die()
    {
        Debug.Log("玩家死亡！");

        // 禁用移动脚本
        GetComponent<PlayerMovement>().enabled = false;

        // 冻结刚体，让玩家彻底停住
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
