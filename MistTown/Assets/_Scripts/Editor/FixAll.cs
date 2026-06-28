using UnityEngine;
using UnityEditor;

public class FixAll : MonoBehaviour
{
    [MenuItem("迷雾镇/一键修复所有问题")]
    static void Fix()
    {
        // 1. 给 Player 加 Rigidbody
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb == null) rb = player.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.mass = 1;

            // 确保 PlayerMovement 在 Player 身上
            if (player.GetComponent<PlayerMovement>() == null)
                player.AddComponent<PlayerMovement>();

            // 确保 CameraSwitch 在 Player 身上
            if (player.GetComponent<CameraSwitch>() == null)
                player.AddComponent<CameraSwitch>();
        }

        // 2. 修复摄像机标签 + 挂 Shoot
        Camera cam = player != null ? player.GetComponentInChildren<Camera>() : FindObjectOfType<Camera>();
        if (cam != null)
        {
            cam.gameObject.tag = "MainCamera";
            if (cam.GetComponent<AudioListener>() == null)
                cam.gameObject.AddComponent<AudioListener>();

            // 挂 Shoot 脚本
            Shoot shoot = cam.GetComponent<Shoot>();
            if (shoot == null) shoot = cam.gameObject.AddComponent<Shoot>();
        }

        // 3. 给 Bullet 预制体加 Rigidbody + Bullet 脚本
        GameObject bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Prefabs/Bullet.prefab");
        if (bulletPrefab != null)
        {
            // 加 Rigidbody
            Rigidbody rb = bulletPrefab.GetComponent<Rigidbody>();
            if (rb == null) rb = bulletPrefab.AddComponent<Rigidbody>();
            rb.useGravity = false;

            // 加 Bullet 脚本
            if (bulletPrefab.GetComponent<Bullet>() == null)
                bulletPrefab.AddComponent<Bullet>();

            // 设置子弹预制体引用
            if (cam != null)
            {
                Shoot shoot = cam.GetComponent<Shoot>();
                if (shoot != null) shoot.bulletPrefab = bulletPrefab;
            }

            // 保存预制体修改
            PrefabUtility.SavePrefabAsset(bulletPrefab);
        }

        // 4. 移除 Global Volume 上的 PlayerMovement（如果误挂了）
        GameObject globalVolume = GameObject.Find("Global Volume");
        if (globalVolume != null)
        {
            PlayerMovement pm = globalVolume.GetComponent<PlayerMovement>();
            if (pm != null) DestroyImmediate(pm);
        }

        Debug.Log("✅ 一键修复完成！按 ▶ Play 试试 WASD + 空格 + 鼠标左键");
    }
}
