using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义一个脚本可创建对象，用于存储和管理物品数据列表
[CreateAssetMenu(fileName = "ItemDataListSO", menuName = "Inventory/ItemDataListSO")]
public class ItemDataListSO : ScriptableObject
{
    // 存储所有物品数据详情的列表
    public List<ItemDataDetails> itemDataDetailsList;
    
    /**
     * 根据物品名称获取物品数据详情
     * @param itemName 物品的名称
     * @return 匹配物品名称的物品数据详情，如果找不到则返回null
     */
    public ItemDataDetails GetItemDataDetails(ItemName itemName)
    {
        foreach (var itemDataDetails in itemDataDetailsList)
        {
            if (itemDataDetails.itemName == itemName)
            {
                return itemDataDetails;
            }
        }
        return null;
    }
}

// 定义物品数据详情的类，包含物品名称和物品图标
[System.Serializable]
public class ItemDataDetails
{
    // 物品的名称
    public ItemName itemName;
    // 物品的图标
    public Sprite itemSprite;
}