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
        }
    }
}
