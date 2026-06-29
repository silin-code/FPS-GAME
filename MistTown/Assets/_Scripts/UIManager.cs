using UnityEngine;

// UIManager: 显示血量条 + 血量数值，挂在 MainCamera 上
public class UIManager : MonoBehaviour
{
    private PlayerHealth playerHealth; // 玩家的血量脚本

    void Start()
    {
        // 找到玩家身上的 PlayerHealth
        GameObject player = GameObject.Find("Player");
        if (player != null)
            playerHealth = player.GetComponent<PlayerHealth>();
    }

    void OnGUI()
    {
        if (playerHealth == null) return;

        float health = playerHealth.currentHealth;
        float maxHealth = playerHealth.maxHealth;
        float barWidth = 200f;  // 血条宽度
        float barHeight = 20f;  // 血条高度

        // 血条位置（左下角）
        float x = 20;
        float y = Screen.height - barHeight - 20;

        // —— 背景（灰色） ——
        GUI.color = Color.gray;
        GUI.DrawTexture(new Rect(x, y, barWidth, barHeight), Texture2D.whiteTexture);

        // —— 血量（颜色随血量变化） ——
        float ratio = health / maxHealth;
        if (ratio > 0.5f)       GUI.color = Color.green;
        else if (ratio > 0.25f) GUI.color = Color.yellow;
        else                    GUI.color = Color.red;
        GUI.DrawTexture(new Rect(x, y, barWidth * ratio, barHeight), Texture2D.whiteTexture);

        // —— 数值文字 ——
        GUI.color = Color.white;
        GUIStyle style = new GUIStyle();
        style.fontSize = 14;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(x, y, barWidth, barHeight), health + " / " + maxHealth, style);
    }
}
