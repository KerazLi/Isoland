using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Button leftButton,rightButton;
    public SlotUI slotUI;
    public int currentIndex;//显示UI当前物品序号

    private void OnEnable()
    {
        EventHandle.UpdateUIEvent += OnUpdateUIEvenet;
    }

    private void OnDisable()
    {
        EventHandle.UpdateUIEvent -= OnUpdateUIEvenet;
    }

    private void OnUpdateUIEvenet(ItemDataDetails itemDataDetails, int index)
    {
        if (itemDataDetails==null)
        {
            slotUI.SetEmpty();
            currentIndex = -1;
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        else
        {
            currentIndex = index;
            slotUI.SetItem(itemDataDetails);
            if (index>0)
            {
                leftButton.interactable = true;
            }

            if (index==-1)
            {
                leftButton.interactable = false;
                rightButton.interactable = false;
            }
        }
    }
    
    /// <summary>
    /// 根据传入的数量调整当前项的索引，并相应地启用或禁用左右按钮。
    /// 该方法的目的是为了在用户界面中实现导航功能，通过点击左右按钮来切换项。
    ///
    /// @param amount 要调整的索引数量。正数表示向右移动，负数表示向左移动。
    /// </summary>
    public void SwitchItem(int amount)
    {
        // 计算调整后的索引位置 
        var index = currentIndex + amount;

        // 根据新的索引位置决定左右按钮的交互状态
        if (index < currentIndex)
        {
            // 如果新索引小于当前索引，说明需要向左移动，禁用右按钮，启用左按钮 
            leftButton.interactable = false;
            rightButton.interactable = true;
        }
        else if (index > currentIndex)
        {
            // 如果新索引大于当前索引，说明需要向右移动，禁用左按钮，启用右按钮 
            leftButton.interactable = true;
            rightButton.interactable = false;
        }
        else
        {
            // 如果新索引等于当前索引，说明没有移动，同时启用左右按钮
            leftButton.interactable = true;
            rightButton.interactable = true;
        }
        EventHandle.CallChangeItemEvent(index);
    }

}
