using UnityEngine;

// Shoot: 鼠标左键发射子弹
public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab; // 子弹预制体（在 Inspector 拖入）
    public float shootForce = 20f;  // 子弹射出速度

    void Update()
    {
        // 鼠标左键按下时开火
        if (Input.GetMouseButtonDown(0))
        {
            // 在摄像机前方 1 米处生成子弹（避免撞到玩家自己的碰撞体）
            Vector3 spawnPos = transform.position + transform.forward * 1f;
            GameObject bullet = Instantiate(bulletPrefab, spawnPos, transform.rotation);

            // 给子弹一个向前的力
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce, ForceMode.Impulse);
        }
    }
}
