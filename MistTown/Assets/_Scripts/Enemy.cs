using UnityEngine;

// Enemy: 当玩家进入检测范围时，敌人会追向玩家，碰到玩家会造成伤害
public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;      // 追击速度
    public float detectRange = 8f;    // 检测范围（玩家进入这个距离就开始追）
    public float damageAmount = 10f;  // 每次碰撞造成的伤害

    private Transform player;         // 玩家的 Transform

    void Start()
    {
        // 先按标签找玩家，找不到就按名字找
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
            playerObj = GameObject.Find("Player");

        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        // 没找到玩家就跳过
        if (player == null) return;

        // 计算敌人到玩家的距离
        float distance = Vector3.Distance(transform.position, player.position);

        // 如果在检测范围内，就追
        if (distance <= detectRange)
        {
            // 朝玩家方向移动
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // 面向玩家
            transform.LookAt(player);
        }
    }

    // 碰到玩家时造成伤害
    void OnCollisionEnter(Collision collision)
    {
        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);
        }
    }
}
