using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public ItemDataListSO itemDataListSO;
    
    [SerializeField]
    private List<ItemName> itemList = new();

    private void OnEnable()
    {
        EventHandle.ItemUseEvent += OnItemUseEvent;
    }

    private void OnDisable()
    {
        EventHandle.ItemUseEvent += OnItemUseEvent;
    }

    private void OnItemUseEvent(ItemName itemName)
    {
        var index = GetItemIndex(itemName);
        itemList.RemoveAt(index);
        //TODO:单一物品的使用
        if (itemList.Count==0)
        {
            EventHandle.CallUpdateUIEvent(null,-1);
        }
    }

    public void AddItem(ItemName itemName)
    {
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            //UI显示
            EventHandle.CallUpdateUIEvent(itemDataListSO.GetItemDataDetails(itemName),itemList.Count-1);
        }
    }

    private int GetItemIndex(ItemName itemName)
    {
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
