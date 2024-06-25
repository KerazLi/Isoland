using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataListSO",menuName = "Inventory/ItemDataListSO")]
public class ItemDataListSO : ScriptableObject
{
    public List<ItemDataDetails> itemDataDetailsList;
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

[System.Serializable]
public class ItemDataDetails
{
    public ItemName itemName;
    public Sprite itemSprite;
}
