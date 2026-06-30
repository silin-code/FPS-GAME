using UnityEngine;

// Shoot: 鼠标左键发射子弹 + 弹药管理 + R 键换弹
public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab; // 子弹预制体
    public float shootForce = 20f;  // 子弹速度

    public int maxAmmo = 12;        // 弹夹容量
    public float reloadTime = 1.5f; // 换弹时间（秒）

    public int currentAmmo { get; private set; } // 当前弹夹子弹数（UI 可读）
    public bool isReloading { get; private set; } // 是否正在换弹（UI 可读）

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        // 按 R 开始换弹
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        // 换弹中不能开枪
        if (isReloading) return;

        // 左键射击
        if (Input.GetMouseButtonDown(0))
        {
            if (currentAmmo <= 0)
            {
                // 没子弹了自动换弹
                StartCoroutine(Reload());
                return;
            }

            // 扣子弹
            currentAmmo--;

            // 生成子弹
            Vector3 spawnPos = transform.position + transform.forward * 1f;
            GameObject bullet = Instantiate(bulletPrefab, spawnPos, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce, ForceMode.Impulse);
        }
    }

    // 换弹协程
    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
