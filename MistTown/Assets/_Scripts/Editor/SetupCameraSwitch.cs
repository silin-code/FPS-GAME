using UnityEngine;
using UnityEditor;

public class SetupCameraSwitch : MonoBehaviour
{
    [MenuItem("迷雾镇/添加视角切换功能")]
    static void Install()
    {
        GameObject player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("找不到 Player，请先搭建测试场景");
            return;
        }

        // 给 Player 挂 CameraSwitch
        if (player.GetComponent<CameraSwitch>() == null)
            player.AddComponent<CameraSwitch>();

        // 设置摄像机标签
        Camera cam = player.GetComponentInChildren<Camera>();
        if (cam != null)
            cam.gameObject.tag = "MainCamera";

        Debug.Log("✅ 已添加 CameraSwitch，按 V 键切换视角");
    }
}
