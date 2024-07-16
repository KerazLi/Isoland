using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * 背包槽位的UI控件，用于显示物品图标和处理与物品相关的交互事件。
 * 实现了IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler接口，以处理鼠标点击、进入、离开事件。
 */
public class SlotUI : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    // 显示物品图标的Image组件。
    public Image itemImage;
    // 物品提示工具提示的UI组件。
    public ItemToolTip toolTip;
    // 当前槽位中的物品数据。
    [SerializeField]private ItemDataDetails currentItem;
    // 指示当前槽位的物品是否被选中的状态。
    private bool isSelected;

    /**
     * 设置槽位中的物品。
     * @param itemDataDetails 设置的物品数据详情。
     */
    public void SetItem(ItemDataDetails itemDataDetails)
    {
        currentItem = itemDataDetails;
        gameObject.SetActive(true);
        itemImage.sprite = currentItem.itemSprite;
        itemImage.SetNativeSize();
    }

    /**
     * 将槽位设置为空闲状态，即不显示任何物品。
     */
    public void SetEmpty()
    {
        gameObject.SetActive(false);
    }

    /**
     * 处理鼠标点击事件。
     * 切换当前物品的选中状态，并触发物品选中事件。
     * @param eventData 事件数据，包含鼠标点击的信息。
     */
    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected;
        EventHandle.CallItemSelectEvent(currentItem, isSelected);
    }

    /**
     * 处理鼠标进入槽位区域的事件。
     * 如果槽位中的物品存在且槽位处于激活状态，则显示物品的工具提示。
     * @param eventData 事件数据，包含鼠标进入的信息。
     */
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.activeInHierarchy)
        {
            toolTip.gameObject.SetActive(true);
            toolTip.UpdateItemName(currentItem.itemName);
        }
    }

    /**
     * 处理鼠标离开槽位区域的事件。
     * 隐藏物品的工具提示。
     * @param eventData 事件数据，包含鼠标离开的信息。
     */
    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.gameObject.SetActive(false);
    }
}