using UnityEngine; // 引入 Unity 功能库，让我们能用 Input、Vector3 等类

// MonoBehaviour: Unity 脚本的基类，让脚本可以挂到游戏物体上
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度。public 可在 Unity 编辑器中直接修改

    void Update() // 每帧自动调用一次（≈ 每秒 60 次）
    {
        // Horizontal = A(-1) / D(+1)，Vertical = W(+1) / S(-1)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 计算移动量：方向 × 输入值 × 速度 × 帧间隔时间
        Vector3 move = (transform.right * x + transform.forward * z) * moveSpeed * Time.deltaTime;

        // 把移动量加到当前位置上
        transform.position += move;
    }
}
