using UnityEngine;

// CameraSwitch: 按 V 键切换第一人称 / 第三人称视角
public class CameraSwitch : MonoBehaviour
{
    public bool isThirdPerson = false;     // 当前是否是第三人称

    public Vector3 firstPersonPos = new Vector3(0, 0.5f, 0);   // 第一人称：摄像机在头部位置
    public Vector3 thirdPersonPos = new Vector3(0, 1.5f, -3f); // 第三人称：摄像机在后方

    public float switchSpeed = 5f; // 切换时的过渡速度

    private Transform camTransform; // 摄像机的 Transform

    void Start()
    {
        // 获取场景中的主摄像机
        camTransform = Camera.main.transform;
    }

    void Update()
    {
        // 按 V 键切换人称
        if (Input.GetKeyDown(KeyCode.V))
        {
            isThirdPerson = !isThirdPerson; // 取反：true ↔ false
        }

        // 根据当前人称决定目标位置
        Vector3 targetPos = isThirdPerson ? thirdPersonPos : firstPersonPos;

        // 平滑移动到目标位置（Lerp = 线性插值）
        camTransform.localPosition = Vector3.Lerp(
            camTransform.localPosition, targetPos, switchSpeed * Time.deltaTime
        );
    }
}
