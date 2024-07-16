using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// InventoryUI 类负责管理游戏中的物品栏用户界面。
/// 它包括左右按钮用于导航物品栏，以及一个物品槽用于显示当前选中的物品。
/// </summary>
public class InventoryUI : MonoBehaviour
{
    /// <summary>
    /// 左按钮，用于选择前一个物品。
    /// </summary>
    public Button leftButton;
    /// <summary>
    /// 右按钮，用于选择下一个物品。
    /// </summary>
    public Button rightButton;
    /// <summary>
    /// 物品槽UI，用于显示当前选中的物品。
    /// </summary>
    public SlotUI slotUI;
    /// <summary>
    /// 当前选中物品的索引。
    /// </summary>
    public int currentIndex;

    /// <summary>
    /// 组件启用时，注册更新UI事件的处理程序。
    /// </summary>
    private void OnEnable()
    {
        EventHandle.UpdateUIEvent += OnUpdateUIEvenet;
    }

    /// <summary>
    /// 组件禁用时，取消注册更新UI事件的处理程序。
    /// </summary>
    private void OnDisable()
    {
        EventHandle.UpdateUIEvent -= OnUpdateUIEvenet;
    }

    /// <summary>
    /// 处理更新UI事件，根据传入的物品数据和索引更新物品栏UI。
    /// </summary>
    /// <param name="itemDataDetails">当前选中物品的数据详情。</param>
    /// <param name="index">当前选中物品的索引。</param>
    private void OnUpdateUIEvenet(ItemDataDetails itemDataDetails, int index)
    {
        if (itemDataDetails == null)
        {
            // 如果物品数据为空，则清空物品槽并禁用左右按钮。
            slotUI.SetEmpty();
            currentIndex = -1;
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        else
        {
            // 如果物品数据不为空，则更新当前索引和物品槽显示，并根据索引更新按钮的交互状态。
            currentIndex = index;
            slotUI.SetItem(itemDataDetails);
            if (index > 0)
            {
                leftButton.interactable = true;
            }
            if (index == -1)
            {
                leftButton.interactable = false;
                rightButton.interactable = false;
            }
        }
    }

    /// <summary>
    /// 根据传入的数量调整当前选中物品的索引，并更新按钮的交互状态。
    /// </summary>
    /// <param name="amount">要调整的索引数量，正数表示向右移动，负数表示向左移动。</param>
    public void SwitchItem(int amount)
    {
        // 计算调整后的索引
        var index = currentIndex + amount;

        // 根据调整后的索引更新按钮的交互状态
        if (index < currentIndex)
        {
            leftButton.interactable = false;
            rightButton.interactable = true;
        }
        else if (index > currentIndex)
        {
            leftButton.interactable = true;
            rightButton.interactable = false;
        }
        else
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
        }
        // 触发切换物品事件
        EventHandle.CallChangeItemEvent(index);
    }
}
