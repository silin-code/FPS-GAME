using UnityEngine;

// Shoot: 左键射击 + 弹药管理 + R 键换弹 + 武器切换 + 自动/散射
public class Shoot : MonoBehaviour
{
    public Weapon[] weapons;
    public GameObject bulletPrefab;

    private int currentIndex = 0;
    private int[] ammoPerWeapon;
    private float lastFireTime;            // 上次射击时间（用于控制射速）
    public int currentAmmo { get; private set; }
    public int maxAmmo { get; private set; }
    public string weaponName { get; private set; }
    public bool isReloading { get; private set; }

    void Start()
    {
        if (weapons == null || weapons.Length == 0) return;

        ammoPerWeapon = new int[weapons.Length];
        for (int i = 0; i < weapons.Length; i++)
            ammoPerWeapon[i] = weapons[i].maxAmmo;

        SwitchWeapon(0);
    }

    void Update()
    {
        // 数字键切换武器
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchWeapon(i);
                return;
            }
        }

        if (weapons.Length == 0) return;
        Weapon w = weapons[currentIndex];

        // 换弹
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload(w.reloadTime));
            return;
        }

        if (isReloading) return;

        // 是否按下射击键
        bool firePressed = w.isAutomatic ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0);
        if (!firePressed) return;

        // 射速控制
        if (Time.time - lastFireTime < w.fireRate) return;

        // 没子弹了
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload(w.reloadTime));
            return;
        }

        // 开火！
        currentAmmo--;
        ammoPerWeapon[currentIndex] = currentAmmo;
        lastFireTime = Time.time;

        for (int i = 0; i < w.bulletsPerShot; i++)
        {
            // 计算子弹方向（带散射）
            Vector3 direction = transform.forward;
            if (w.spreadAngle > 0)
            {
                float spreadRad = w.spreadAngle * Mathf.Deg2Rad;
                direction = transform.forward + Random.insideUnitSphere * spreadRad;
                direction.Normalize();
            }

            Vector3 spawnPos = transform.position + transform.forward * 1f;
            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.LookRotation(direction));
            bullet.GetComponent<Rigidbody>().AddForce(direction * w.shootForce, ForceMode.Impulse);

            // 传给子弹伤害值
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null) bulletScript.damageAmount = w.damage;
        }
    }

    void SwitchWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;
        if (index == currentIndex) return;

        currentIndex = index;
        currentAmmo = ammoPerWeapon[index];
        maxAmmo = weapons[index].maxAmmo;
        weaponName = weapons[index].weaponName;
    }

    System.Collections.IEnumerator Reload(float time)
    {
        isReloading = true;
        yield return new WaitForSeconds(time);
        ammoPerWeapon[currentIndex] = weapons[currentIndex].maxAmmo;
        currentAmmo = ammoPerWeapon[currentIndex];
        isReloading = false;
    }
}
