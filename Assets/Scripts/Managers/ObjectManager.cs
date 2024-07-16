using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ObjectManager 类负责管理场景中的物体状态，包括物品的可用性和交互状态的管理。
 * 它通过订阅和处理场景加载/卸载事件来更新物体的状态。
 */
public class ObjectManager : MonoBehaviour
{
    // 存储物品的可用性状态，键为物品名称，值为物品是否可用。
    private Dictionary<ItemName, bool> itemAvailableDict = new();
    // 存储交互物体的状态，键为交互物体名称，值为交互是否已完成。
    private Dictionary<string, bool> interactiveStateDict = new();

    /**
     * 当此组件启用时，订阅相关场景事件。
     * 这确保了在场景加载或卸载时可以正确地更新物体状态。
     */
    private void OnEnable()
    {
        EventHandle.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandle.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
        EventHandle.UpdateUIEvent += OnUpdateUIEvent;
    }
    
    /**
     * 当此组件禁用时，取消订阅相关场景事件。
     * 这避免了在组件不再使用时仍尝试更新物体状态，从而减少不必要的处理。
     */
    private void OnDisable()
    {
        EventHandle.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandle.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
        EventHandle.UpdateUIEvent -= OnUpdateUIEvent;
    }

    /**
     * 场景加载后调用此方法，用于初始化或更新物品的可用性和交互物体的状态。
     */
    private void OnAfterSceneLoadEvent()
    {
        // 遍历场景中的所有物品，初始化或更新它们的可用性状态。
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
            {
                itemAvailableDict.Add(item.itemName, true);
            }
            else
            {
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);
            }
        }
        // 遍历场景中的所有交互物体，初始化或更新它们的交互状态。
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
            {
                item.isDone = interactiveStateDict[item.name];
            }
            else
            {
                interactiveStateDict.Add(item.name, item.isDone);
            }
        }
    }
    
    /**
     * 场景卸载前调用此方法，用于保存物品的可用性和交互物体的状态。
     */
    private void OnBeforeSceneUnloadEvent()
    {
        // 遍历场景中的所有物品，保存它们的可用性状态。
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
            {
                itemAvailableDict.Add(item.itemName, true);
            }
        }
        // 遍历场景中的所有交互物体，保存它们的交互状态。
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
            {
                interactiveStateDict[item.name] = item.isDone;
            }
            else
            {
                interactiveStateDict.Add(item.name, item.isDone);
            }
        }
    }
    
    /**
     * 当UI更新事件发生时调用此方法，用于更新物品的可用性状态。
     * @param itemDataDetails 包含物品信息的详细数据对象。
     * @param arg2 该参数未在注释中说明其用途。
     */
    private void OnUpdateUIEvent(ItemDataDetails itemDataDetails, int arg2)
    {
        // 如果提供了物品数据详情，则将该物品标记为不可用。
        if (itemDataDetails != null)
        {
            itemAvailableDict[itemDataDetails.itemName] = false;
        }
    }
}
