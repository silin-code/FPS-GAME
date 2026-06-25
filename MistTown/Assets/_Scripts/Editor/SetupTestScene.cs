using UnityEngine;
using UnityEditor;

public class SetupTestScene : MonoBehaviour
{
    [MenuItem("迷雾镇/搭建测试场景")]
    static void BuildTestScene()
    {
        // 删除旧东西
        GameObject oldPlayer = GameObject.Find("Player");
        if (oldPlayer != null) Object.DestroyImmediate(oldPlayer);

        // 1. 创建地面
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.position = new Vector3(0, 0, 0);

        // 2. 创建 Player（方块）
        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Cube);
        player.name = "Player";
        player.transform.position = new Vector3(0, 1, 0);

        // 3. 添加 PlayerMovement
        player.AddComponent<PlayerMovement>();

        // 4. 创建摄像机并挂到 Player 下
        GameObject camObj = new GameObject("MainCamera");
        camObj.AddComponent<Camera>();
        camObj.AddComponent<AudioListener>();
        camObj.transform.SetParent(player.transform);
        camObj.transform.localPosition = new Vector3(0, 0.5f, 0);

        // 5. 添加 MouseLook 并设置 playerBody
        MouseLook ml = camObj.AddComponent<MouseLook>();
        ml.playerBody = player.transform;

        // 6. 创建参照物（几根柱子，方便看移动）
        for (int i = 0; i < 4; i++)
        {
            GameObject pillar = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            pillar.name = "Pillar";
            pillar.transform.position = new Vector3(i % 2 == 0 ? 5 : -5, 0.5f, i < 2 ? 5 : -5);
            pillar.transform.localScale = new Vector3(0.5f, 1, 0.5f);
        }

        // 7. 加一点灯光
        Light light = new GameObject("Directional Light").AddComponent<Light>();
        light.type = LightType.Directional;
        light.transform.rotation = Quaternion.Euler(50, -30, 0);

        Cursor.lockState = CursorLockMode.Locked;

        Debug.Log("✅ 测试场景搭建完成！按 ▶ Play，用 WASD + 鼠标试试");
    }
}
