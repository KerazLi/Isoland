using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public ItemDataListSO itemDataListSO;
    
    [SerializeField]
    private List<ItemName> itemList = new();
    public void AddItem(ItemName itemName)
    {
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            //UI显示
            EventHandle.CallUpdateUIEvent(itemDataListSO.GetItemDataDetails(itemName),itemList.Count-1);
        }
    }
}
