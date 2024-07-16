using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 交互类，用于处理游戏中的交互逻辑。
 * 该类定义了与物品交互的基本行为，并提供了扩展点以供子类定制特定的交互行为。
 */
public class Interactive : MonoBehaviour
{
    // 需要的物品，用于检查玩家是否持有正确的物品来触发交互。
    public ItemName requireItem;
    // 交互是否已经完成的标志，用于防止重复交互。
    public bool isDone;

    /**
     * 检查玩家是否持有正确的物品，并处理交互。
     * 如果玩家持有正确的物品且交互尚未完成，则标记交互为完成，并触发交互动作。
     *
     * @param itemName 玩家当前持有的物品名称。
     */
    public void CheckItem(ItemName itemName)
    {
        if (itemName == requireItem && !isDone)
        {
            isDone = true;
            OnclickedAction();
            EventHandle.CallItemUseEvent(itemName);
        }
    }

    /**
     * 交互动作的虚拟方法，子类可以重写以定义特定的交互行为。
     * 当玩家与游戏对象交互时，此方法将被调用。
     */
    public virtual void OnclickedAction()
    {
    }

    /**
     * 空交互的虚拟方法，子类可以重写以定义在没有正确物品时的交互行为。
     * 当玩家没有持有正确的物品时，此方法将被调用。
     */
    public virtual void EmptyClicked()
    {
    }
}