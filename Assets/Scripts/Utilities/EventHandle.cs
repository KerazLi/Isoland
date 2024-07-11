using System;


public static class EventHandle
{
    public static event Action<ItemDataDetails, int> UpdateUIEvent;

    public static void CallUpdateUIEvent(ItemDataDetails itemDataDetails, int index)
    {
        UpdateUIEvent?.Invoke(itemDataDetails, index);
    }

    public static event Action BeforeSceneUnloadEvent;
    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }
    public static  event Action AfterSceneLoadEvent;
    public static void CallAfterSceneLoadEvent()
    {
        AfterSceneLoadEvent?.Invoke();
    }

    public static event Action<ItemDataDetails, bool> ItemSelectEvent;
    public static void CallItemSelectEvent(ItemDataDetails itemDataDetails, bool isSelected)
    {
        ItemSelectEvent?.Invoke(itemDataDetails, isSelected);
    }
    public static  event Action<ItemName> ItemUseEvent;
    public static void CallItemUseEvent(ItemName itemName)
    {
        ItemUseEvent?.Invoke(itemName);
    }
}
