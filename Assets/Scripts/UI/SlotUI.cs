using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public Image itemImage;
    [SerializeField]private ItemDataDetails currentItem;
    private bool isSelected;
    public void SetItem(ItemDataDetails itemDataDetails)
    {
        currentItem = itemDataDetails;
        gameObject.SetActive(true);
        itemImage.sprite = currentItem.itemSprite;
        itemImage.SetNativeSize();
    }

    public void SetEmpty()
    {
        gameObject.SetActive(false);
    }
}
