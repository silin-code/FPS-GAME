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
        // 暂时禁用玩家移动
        GetComponent<PlayerMovement>().enabled = false;
    }
}
