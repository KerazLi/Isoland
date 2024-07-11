using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public RectTransform hand;
    private Vector3 mouseWordPosition;
    private ItemName currentItem;
    private bool canClick;
    private bool holdItem;

    private void OnEnable()
    {
        EventHandle.ItemSelectEvent += OnItemSelectEvent;
        EventHandle.ItemUseEvent += OnItemUseEvent;
    }

    private void OnDisable()
    {
        EventHandle.ItemSelectEvent -= OnItemSelectEvent;
        EventHandle.ItemUseEvent -= OnItemUseEvent;
    }

    


    private void Update()
    {
        mouseWordPosition =  Camera.main.ScreenToWorldPoint(new (Input.mousePosition.x,Input.mousePosition.y,0 ));
        canClick = ObjectAtMouserPosition();
        if (hand.gameObject.activeInHierarchy)
        {
            hand.position = Input.mousePosition;
        }
        if (canClick & Input.GetMouseButtonDown(0))
        {
            ClickAction(ObjectAtMouserPosition().gameObject);
        }
    }

    private void ClickAction(GameObject clickObject)
    {
        switch (clickObject.tag)
        {
            case "Teleport":
                var teleport=clickObject.GetComponent<Teleport>();
                teleport?.TeleportScene();
                break;
            case "Item":
                var  item=clickObject.GetComponent<Item>();
                item?.ItemClick();
                break;
            case "Interactive":
                var interactive=clickObject.GetComponent<Interactive>();
                if (holdItem)
                {
                    interactive?.CheckItem(currentItem);
                }
                else
                {
                    interactive?.EmptyClicked();
                }

                break;
        }
    }

    private Collider2D ObjectAtMouserPosition()
    {
        return Physics2D.OverlapPoint(mouseWordPosition);
    }
    private void OnItemSelectEvent(ItemDataDetails itemDataDetails, bool isSelect)
    {
        holdItem = isSelect;
        if (isSelect)
        {
            currentItem = itemDataDetails.itemName;
        }
        hand.gameObject.SetActive(holdItem);
    }
    private void OnItemUseEvent(ItemName obj)
    {
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(holdItem);
    }
}
