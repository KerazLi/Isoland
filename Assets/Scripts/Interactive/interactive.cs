using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requireItem;
    public bool isDone;

    public void CheckItem(ItemName itemName)
    {
        if (itemName==requireItem&&!isDone)
        {
            isDone = true;
            OnclickedAction();
            EventHandle.CallItemUseEvent(itemName);
        }
        
    }

    public virtual void OnclickedAction()
    {
        
    }
    public virtual void EmptyClicked()
    {
        Debug.Log("空点");
    }
}
