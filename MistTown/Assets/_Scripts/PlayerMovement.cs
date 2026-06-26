using UnityEngine;

// PlayerMovement: 处理 WASD 移动 + 空格跳跃
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;   // 移动速度。public 可在 Unity 编辑器中修改
    public float jumpForce = 5f;   // 跳跃力度

    private Rigidbody rb;          // 刚体组件引用（用于重力 + 跳跃）
    private bool isGrounded;       // 是否站在地面上，防止空中连跳

    void Start()
    {
        // 获取挂在这个物体上的 Rigidbody 组件
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // —— 水平移动 ——
        float x = Input.GetAxis("Horizontal"); // A(-1) / D(+1)
        float z = Input.GetAxis("Vertical");   // W(+1) / S(-1)

        // 计算移动方向 × 速度（不含 Y 轴）
        Vector3 move = (transform.right * x + transform.forward * z) * moveSpeed;
        // 保留刚体当前的 Y 速度（重力下落效果），只修改 X 和 Z
        move.y = rb.velocity.y;
        // 将速度赋给刚体，替代之前的 transform.position
        rb.velocity = move;

        // —— 跳跃 ——
        // 按下空格 且 站在地面上
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Y 轴速度设为跳越力，X 和 Z 保持不变
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    // 当碰撞体持续接触时（站在地面上），标记为在地面
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    // 当碰撞体离开时（跳起来或走出悬崖），取消地面标记
    void OnCollisionExit()
    {
        isGrounded = false;
    }
}
