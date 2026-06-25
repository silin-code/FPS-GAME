using System;
using UnityEngine;//引入Unity功能库

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;//鼠标灵敏度，可在编辑器调

    public Transform playerBody;//玩家身体(用于左右旋转)

    private float xRotation = 0f;//记录上下旋转视角，限制视角范围

    void Start()//游戏开始时执行一次
    {
        //锁定鼠标到屏幕中央,按Esc可释放
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()//每帧执行一次
    {
        //获取鼠标移动量   灵敏度  帧间隔
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //上下旋转(鼠标Y轴)--限制角度防止头翻过去
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);//限制上下90度

        //应用上下旋转到摄像机(当前物体)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //左右旋转(鼠标X轴)--旋转整个身体
        playerBody.Rotate(Vector3.up * mouseX);
    }
}