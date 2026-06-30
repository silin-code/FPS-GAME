using UnityEngine;

// Weapon: 武器数据定义，挂在武器预制体上
public class Weapon : MonoBehaviour
{
    public string weaponName = "手枪";        // 武器名字
    public int maxAmmo = 12;                 // 弹夹容量
    public float shootForce = 20f;           // 子弹速度
    public float reloadTime = 1.5f;          // 换弹时间
    public float damage = 10f;               // 单发伤害
    public int bulletsPerShot = 1;           // 每次射击射出几颗子弹（霰弹）
    public float spreadAngle = 0f;           // 散射角度（霰弹枪用）
    public bool isAutomatic = false;         // 是否按住连发（步枪）
    public float fireRate = 0.2f;            // 射击间隔（秒），连发用
}
