using UnityEngine;

// HealthPickup: 血包，继承自 Interactable
// 玩家按 E 拾取后回血，然后销毁自己
public class HealthPickup : Interactable
{
    public int healAmount = 30; // 回复血量

    void Start()
    {
        // 设置提示文字，覆盖父类的默认值
        promptText = "按 E 拾取血包 (+" + healAmount + ")";
    }

    // 重写父类的 Interact 方法，实现具体的血包行为
    public override void Interact()
    {
        // 找到玩家并回血
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Heal(healAmount);
                Debug.Log("回血 " + healAmount + " 点！");
            }
        }

        Destroy(gameObject); // 拾取后销毁
    }
}
