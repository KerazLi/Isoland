using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private Vector3 mouseWordPosition;
    private bool canClick;
    

    private void Update()
    {
        mouseWordPosition =  Camera.main.ScreenToWorldPoint(new (Input.mousePosition.x,Input.mousePosition.y,0 ));
        canClick = ObjectAtMouserPosition();
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
        }
    }

    private Collider2D ObjectAtMouserPosition()
    {
        return Physics2D.OverlapPoint(mouseWordPosition);
    }
}
