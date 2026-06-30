using UnityEngine;

// UIManager: 显示血量条 + 弹药信息，挂在 MainCamera 上
public class UIManager : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Shoot playerShoot;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            // Shoot 在摄像机（MainCamera）上
            playerShoot = GetComponent<Shoot>();
        }
    }

    void OnGUI()
    {
        if (playerHealth == null) return;

        DrawHealthBar();
        DrawAmmo();
    }

    void DrawHealthBar()
    {
        float health = playerHealth.currentHealth;
        float maxHealth = playerHealth.maxHealth;
        float barWidth = 200f;
        float barHeight = 20f;

        float x = 20;
        float y = Screen.height - barHeight - 20;

        // 背景（灰色）
        GUI.color = Color.gray;
        GUI.DrawTexture(new Rect(x, y, barWidth, barHeight), Texture2D.whiteTexture);

        // 血量条（绿→黄→红）
        float ratio = health / maxHealth;
        if (ratio > 0.5f)       GUI.color = Color.green;
        else if (ratio > 0.25f) GUI.color = Color.yellow;
        else                    GUI.color = Color.red;
        GUI.DrawTexture(new Rect(x, y, barWidth * ratio, barHeight), Texture2D.whiteTexture);

        // 数值
        GUI.color = Color.white;
        GUIStyle style = new GUIStyle();
        style.fontSize = 14;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(x, y, barWidth, barHeight), health + " / " + maxHealth, style);
    }

    void DrawAmmo()
    {
        if (playerShoot == null) return;

        // 右下角显示弹药
        float x = Screen.width - 150;
        float y = Screen.height - 50;

        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleRight;

        string ammoText = playerShoot.currentAmmo + " / " + playerShoot.maxAmmo;
        if (playerShoot.isReloading)
            ammoText = "换弹中...";

        GUI.Label(new Rect(x, y, 130, 40), ammoText, style);
    }
}
