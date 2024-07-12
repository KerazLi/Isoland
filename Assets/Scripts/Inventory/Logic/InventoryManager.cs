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
        EventHandle.ChangeItemEvnet += OnChangeItemEvnet;
        EventHandle.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
    }

    private void OnDisable()
    {
        EventHandle.ItemUseEvent -= OnItemUseEvent;
        EventHandle.ChangeItemEvnet -= OnChangeItemEvnet;
        EventHandle.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
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
    private void OnChangeItemEvnet(int index)
    {
        if (index>=0&&index<itemList.Count)
        {
            ItemDataDetails itemDataDetails = itemDataListSO.GetItemDataDetails(itemList[index]);
            EventHandle.CallUpdateUIEvent(itemDataDetails,index);
        }
    }
    private void OnAfterSceneLoadEvent()
    {
        if (itemList.Count==0)
        {
            EventHandle.CallUpdateUIEvent(null,-1);
        }else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                EventHandle.CallUpdateUIEvent(itemDataListSO.GetItemDataDetails(itemList[i]),i);
            }
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
