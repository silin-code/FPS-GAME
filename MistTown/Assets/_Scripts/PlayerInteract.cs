using UnityEngine;

// PlayerInteract: 玩家按 E 键交互 + 屏幕准心 + 交互提示
public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 3f; // 交互距离

    private Interactable currentTarget; // 当前对准的可交互物体
    private string currentPrompt = "";  // 当前显示的提示文字

    void Update()
    {
        // 默认清空提示
        currentTarget = null;
        currentPrompt = "";

        // 从摄像机向前发射射线
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactRange))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                currentTarget = interactable;
                currentPrompt = interactable.promptText;

                // 按 E 交互
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
    }

    // OnGUI: Unity 自带的旧版 UI 绘制，适合简单的准心和文字
    void OnGUI()
    {
        // —— 画准心（一个小十字） ——
        float centerX = Screen.width / 2f;
        float centerY = Screen.height / 2f;

        GUI.color = Color.white;
        // 横线
        GUI.DrawTexture(new Rect(centerX - 5, centerY - 1, 10, 2), Texture2D.whiteTexture);
        // 竖线
        GUI.DrawTexture(new Rect(centerX - 1, centerY - 5, 2, 10), Texture2D.whiteTexture);

        // —— 画交互提示 ——
        if (currentPrompt != "")
        {
            GUI.color = Color.yellow;
            GUIStyle style = new GUIStyle();
            style.fontSize = 16;
            style.normal.textColor = Color.yellow;
            style.alignment = TextAnchor.MiddleCenter;

            // 屏幕中央偏下位置显示提示
            GUI.Label(new Rect(0, centerY + 30, Screen.width, 30), currentPrompt, style);
        }
    }
}
