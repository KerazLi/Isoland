using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 物品管理器类，负责管理玩家的物品库存并处理相关事件。
 * 继承自Singleton<InventoryManager>，确保只有一个实例存在。
 */
public class InventoryManager : Singleton<InventoryManager>
{
    // 存储所有物品数据的脚本ableObject引用。
    public ItemDataListSO itemDataListSO;
    
    // 物品列表，存储玩家当前拥有的物品名称。
    [SerializeField]
    private List<ItemName> itemList = new();

    /**
     * 当组件启用时注册事件处理程序。
     */
    private void OnEnable()
    {
        // 注册物品使用、物品变化和场景加载后的事件处理程序。
        EventHandle.ItemUseEvent += OnItemUseEvent;
        EventHandle.ChangeItemEvnet += OnChangeItemEvnet;
        EventHandle.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
    }

    /**
     * 当组件禁用时取消注册事件处理程序。
     */
    private void OnDisable()
    {
        // 取消注册物品使用、物品变化和场景加载后的事件处理程序。
        EventHandle.ItemUseEvent -= OnItemUseEvent;
        EventHandle.ChangeItemEvnet -= OnChangeItemEvnet;
        EventHandle.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
    }
    
    /**
     * 处理物品使用事件。
     * @param itemName 使用的物品名称。
     */
    private void OnItemUseEvent(ItemName itemName)
    {
        // 获取物品在列表中的索引。
        var index = GetItemIndex(itemName);
        // 从列表中移除该物品。
        itemList.RemoveAt(index);
        // 如果物品列表为空，通知UI更新。
        //TODO:单一物品的使用
        if (itemList.Count==0)
        {
            EventHandle.CallUpdateUIEvent(null,-1);
        }
    }
    
    /**
     * 处理物品变化事件。
     * @param index 变化的物品在列表中的索引。
     */
    private void OnChangeItemEvnet(int index)
    {
        // 检查索引是否有效。
        if (index>=0&&index<itemList.Count)
        {
            // 获取物品数据并通知UI更新。
            ItemDataDetails itemDataDetails = itemDataListSO.GetItemDataDetails(itemList[index]);
            EventHandle.CallUpdateUIEvent(itemDataDetails,index);
        }
    }
    
    /**
     * 处理场景加载后的事件。
     */
    private void OnAfterSceneLoadEvent()
    {
        // 如果物品列表为空，通知UI更新。
        if (itemList.Count==0)
        {
            EventHandle.CallUpdateUIEvent(null,-1);
        }else
        {
            // 遍历物品列表，通知UI更新每个物品。
            for (int i = 0; i < itemList.Count; i++)
            {
                EventHandle.CallUpdateUIEvent(itemDataListSO.GetItemDataDetails(itemList[i]),i);
            }
        }
    }

    /**
     * 向玩家的物品列表中添加物品。
     * @param itemName 要添加的物品名称。
     */
    public void AddItem(ItemName itemName)
    {
        // 如果物品列表中不包含该物品，则添加到列表中并通知UI更新。
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            //UI显示
            EventHandle.CallUpdateUIEvent(itemDataListSO.GetItemDataDetails(itemName),itemList.Count-1);
        }
    }

    /**
     * 获取物品在列表中的索引。
     * @param itemName 物品名称。
     * @return 物品的索引，如果物品不存在则返回-1。
     */
    private int GetItemIndex(ItemName itemName)
    {
        // 遍历列表查找物品的索引。
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i]==itemName)
            {
                return i;
            }
        }

        return -1;
    }
}
