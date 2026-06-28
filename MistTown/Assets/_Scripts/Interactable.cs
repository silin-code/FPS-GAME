using UnityEngine;

// Interactable: 可交互物体的基类。继承它来写具体的交互（捡血包、开门等）
public class Interactable : MonoBehaviour
{
    public string promptText = "按 E 交互"; // 靠近时显示的提示文字

    // Interact: 玩家按 E 时会调用这个方法。子类重写它实现具体功能
    public virtual void Interact()
    {
        // 默认什么都不做，交给子类覆盖
    }
}
