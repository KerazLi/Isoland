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
}
