using UnityEngine;
using UnityEditor;

public class SetupFPS : MonoBehaviour
{
    [MenuItem("迷雾镇/一键设置 FPS 场景")]
    static void SetupScene()
    {
        // 1. 创建 Player（如果不存在）
        GameObject player = GameObject.Find("Player");
        if (player == null)
        {
            player = GameObject.CreatePrimitive(PrimitiveType.Cube);
            player.name = "Player";
        }

        // 2. 添加 PlayerMovement
        if (player.GetComponent<PlayerMovement>() == null)
            player.AddComponent<PlayerMovement>();

        // 3. 创建或找到 Camera
        Camera cam = player.GetComponentInChildren<Camera>();
        GameObject camObj;
        if (cam == null)
        {
            camObj = new GameObject("MainCamera");
            camObj.AddComponent<Camera>();
            camObj.AddComponent<AudioListener>();
        }
        else
        {
            camObj = cam.gameObject;
        }

        // 4. Camera 设为 Player 子物体，重置位置
        camObj.transform.SetParent(player.transform);
        camObj.transform.localPosition = new Vector3(0, 0.5f, 0);
        camObj.transform.localRotation = Quaternion.identity;

        // 5. 添加 MouseLook
        MouseLook ml = camObj.GetComponent<MouseLook>();
        if (ml == null)
            ml = camObj.AddComponent<MouseLook>();
        ml.playerBody = player.transform;

        // 6. 锁定鼠标
        Cursor.lockState = CursorLockMode.Locked;

        Debug.Log("✅ FPS 场景设置完成！按 ▶ Play 试试 WASD + 鼠标");
    }
}
