using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private Dictionary<ItemName, bool> itemAvailableDict = new();
    private Dictionary<string, bool> interactiveStateDict = new();

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
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
            {
                item.isDone=interactiveStateDict[item.name] ;
            }
            else
            {
                interactiveStateDict.Add(item.name,item.isDone);
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

        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
            {
                interactiveStateDict[item.name] = item.isDone;
            }
            else
            {
                interactiveStateDict.Add(item.name,item.isDone);
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
