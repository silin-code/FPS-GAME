using UnityEngine;
using UnityEditor;

public class SetupWeapons : MonoBehaviour
{
    [MenuItem("迷雾镇/一键创建武器")]
    static void Install()
    {
        Camera cam = Camera.main;
        if (cam == null) cam = FindObjectOfType<Camera>();

        Shoot shoot = cam.GetComponent<Shoot>();
        if (shoot == null) shoot = cam.gameObject.AddComponent<Shoot>();

        GameObject weaponFolder = new GameObject("_Weapons");
        weaponFolder.transform.SetParent(cam.transform);

        // 手枪：单发，适中伤害，换弹快
        Weapon pistol = CreateWeapon("手枪", 12, 20f, 1.2f, weaponFolder.transform);
        pistol.damage = 15f;
        pistol.bulletsPerShot = 1;
        pistol.spreadAngle = 0f;
        pistol.isAutomatic = false;

        // 霰弹枪：散射 6 颗，伤害低但范围大，换弹慢
        Weapon shotgun = CreateWeapon("霰弹枪", 6, 25f, 2f, weaponFolder.transform);
        shotgun.damage = 8f;
        shotgun.bulletsPerShot = 6;
        shotgun.spreadAngle = 5f;
        shotgun.isAutomatic = false;

        // 步枪：连发，高伤害，换弹慢
        Weapon rifle = CreateWeapon("步枪", 30, 30f, 2.5f, weaponFolder.transform);
        rifle.damage = 20f;
        rifle.bulletsPerShot = 1;
        rifle.spreadAngle = 0f;
        rifle.isAutomatic = true;
        rifle.fireRate = 0.1f;

        shoot.weapons = new Weapon[] { pistol, shotgun, rifle };
        shoot.bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/_Prefabs/Bullet.prefab");

        Debug.Log("✅ 3 把武器已创建！按 1/2/3 切换");
    }

    static Weapon CreateWeapon(string name, int ammo, float force, float reload, Transform parent)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent);
        Weapon w = obj.AddComponent<Weapon>();
        w.weaponName = name;
        w.maxAmmo = ammo;
        w.shootForce = force;
        w.reloadTime = reload;
        return w;
    }
}
