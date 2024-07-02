using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private Dictionary<ItemName, bool> itemAvailableDict = new();

    private void OnEnable()
    {
        EventHandle.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandle.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        EventHandle.UpdateUIEvent+= OnUpdateUIEvent;
    }
    
    private void OnDisable()
    {
        EventHandle.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandle.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        EventHandle.UpdateUIEvent -= OnUpdateUIEvent;
    }

    private void OnAfterSceneLoadEvent()
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
            {
                itemAvailableDict.Add(item.itemName,true);
            }
            else
            {
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);
            }
        }
    }
    
    private void OnBeforeSceneUnloadEvent()
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
            {
                itemAvailableDict.Add(item.itemName,true);
            }
        }
    }
    private void OnUpdateUIEvent(ItemDataDetails itemDataDetails, int arg2)
    {
        if (itemDataDetails != null)
        {
            itemAvailableDict[itemDataDetails.itemName] = false;
        }
    }
}
