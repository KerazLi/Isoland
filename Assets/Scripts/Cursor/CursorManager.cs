using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 管理游戏中的鼠标指针行为和交互。
 * 通过监听事件和更新逻辑来处理物品选择、使用和交互。
 */
public class CursorManager : MonoBehaviour
{
    // 右手的RectTransform，用于在持有物品时显示。
    public RectTransform hand;
    // 鼠标位置在世界坐标中的预测位置。
    private Vector3 mouseWorldPosition;
    // 当前持有的物品。
    private ItemName currentItem;
    // 指示是否可以进行点击操作。
    private bool canClick;
    // 指示是否正持有物品。
    private bool holdItem;

    /**
     * 组件启用时注册事件处理程序。
     * 订阅ItemSelectEvent和ItemUseEvent事件。
     */
    private void OnEnable()
    {
        EventHandle.ItemSelectEvent += OnItemSelectEvent;
        EventHandle.ItemUseEvent += OnItemUseEvent;
    }

    /**
     * 组件禁用时取消注册事件处理程序。
     * 取消订阅ItemSelectEvent和ItemUseEvent事件。
     */
    private void OnDisable()
    {
        EventHandle.ItemSelectEvent -= OnItemSelectEvent;
        EventHandle.ItemUseEvent -= OnItemUseEvent;
    }

    /**
     * 每帧更新一次，处理鼠标位置和交互逻辑。
     */
    private void Update()
    {
        // 将鼠标屏幕位置转换为世界位置。
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        // 检查鼠标下是否有可交互对象。
        canClick = ObjectAtMousePosition() != null;
        // 如果手部游戏对象激活，将其位置设置为鼠标位置。
        if (hand.gameObject.activeInHierarchy)
        {
            hand.position = Input.mousePosition;
        }
        // 如果可以点击且鼠标左键被按下，执行点击动作。
        if (canClick && Input.GetMouseButtonDown(0))
        {
            ClickAction(ObjectAtMousePosition().gameObject);
        }
    }

    /**
     * 处理点击操作，根据点击的对象执行不同的逻辑。
     * @param clickObject 被点击的游戏对象。
     */
    private void ClickAction(GameObject clickObject)
    {
        switch (clickObject.tag)
        {
            case "Teleport":
                // 如果点击了传送点，进行场景传送。
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportScene();
                break;
            case "Item":
                // 如果点击了物品，触发物品点击事件。
                var item = clickObject.GetComponent<Item>();
                item?.ItemClick();
                break;
            case "Interactive":
                // 如果点击了交互对象，根据是否持有物品执行不同交互。
                var interactive = clickObject.GetComponent<Interactive>();
                if (holdItem)
                {
                    interactive?.CheckItem(currentItem);
                }
                else
                {
                    interactive?.EmptyClicked();
                }
                break;
        }
    }

    /**
     * 在鼠标位置返回第一个碰撞的2D碰撞器。
     * @return 鼠标位置上的碰撞器，如果没有则为null。
     */
    private Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPosition);
    }

    /**
     * 处理物品选择事件。
     * @param itemDataDetails 物品的详细数据。
     * @param isSelect 是否选择物品。
     * 根据事件更新当前持有的物品状态和手部游戏对象的激活状态。
     */
    private void OnItemSelectEvent(ItemDataDetails itemDataDetails, bool isSelect)
    {
        holdItem = isSelect;
        if (isSelect)
        {
            currentItem = itemDataDetails.itemName;
        }
        hand.gameObject.SetActive(holdItem);
    }

    /**
     * 处理物品使用事件。
     * @param obj 使用的物品名称。
     * 清空当前持有的物品和状态，并更新手部游戏对象的激活状态。
     */
    private void OnItemUseEvent(ItemName obj)
    {
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(holdItem);
    }
}
