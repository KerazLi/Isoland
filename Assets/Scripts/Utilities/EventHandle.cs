using System;

/// <summary>
/// 事件处理类，提供了一系列静态事件以供游戏中的不同组件订阅和触发。
/// </summary>
public static class EventHandle
{
    /// <summary>
    /// 更新UI事件，当需要刷新界面元素时触发。
    /// </summary>
    public static event Action<ItemDataDetails, int> UpdateUIEvent;

    /// <summary>
    /// 触发更新UI事件。
    /// </summary>
    /// <param name="itemDataDetails">包含物品数据详情的对象。</param>
    /// <param name="index">需要更新的UI元素的索引。</param>
    public static void CallUpdateUIEvent(ItemDataDetails itemDataDetails, int index)
    {
        UpdateUIEvent?.Invoke(itemDataDetails, index);
    }

    /// <summary>
    /// 场景卸载前事件，用于在场景卸载前执行某些操作。
    /// </summary>
    public static event Action BeforeSceneUnloadEvent;

    /// <summary>
    /// 触发场景卸载前事件。
    /// </summary>
    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    /// <summary>
    /// 场景加载后事件，用于在场景加载完成后执行某些操作。
    /// </summary>
    public static event Action AfterSceneLoadEvent;

    /// <summary>
    /// 触发场景加载后事件。
    /// </summary>
    public static void CallAfterSceneLoadEvent()
    {
        AfterSceneLoadEvent?.Invoke();
    }

    /// <summary>
    /// 物品选择事件，当物品被选中或取消选中时触发。
    /// </summary>
    public static event Action<ItemDataDetails, bool> ItemSelectEvent;

    /// <summary>
    /// 触发物品选择事件。
    /// </summary>
    /// <param name="itemDataDetails">包含物品数据详情的对象。</param>
    /// <param name="isSelected">指示物品是否被选中。</param>
    public static void CallItemSelectEvent(ItemDataDetails itemDataDetails, bool isSelected)
    {
        ItemSelectEvent?.Invoke(itemDataDetails, isSelected);
    }

    /// <summary>
    /// 物品使用事件，当物品被使用时触发。
    /// </summary>
    public static event Action<ItemName> ItemUseEvent;

    /// <summary>
    /// 触发物品使用事件。
    /// </summary>
    /// <param name="itemName">被使用的物品名称。</param>
    public static void CallItemUseEvent(ItemName itemName)
    {
        ItemUseEvent?.Invoke(itemName);
    }

    /// <summary>
    /// 物品变更事件，当物品栏发生变更时触发。
    /// </summary>
    public static event Action<int> ChangeItemEvnet;

    /// <summary>
    /// 触发物品变更事件。
    /// </summary>
    /// <param name="index">变更的物品索引。</param>
    public static void CallChangeItemEvent(int index)
    {
        ChangeItemEvnet?.Invoke(index);
    }

    /// <summary>
    /// 显示对话框事件，当需要显示对话框时触发。
    /// </summary>
    public static event Action<string> ShowDialogueEvent;

    /// <summary>
    /// 触发显示对话框事件。
    /// </summary>
    /// <param name="dialogue">要显示的对话内容。</param>
    public  static void CallShowDialogueEvent(string dialogue)
    {
        ShowDialogueEvent?.Invoke(dialogue);
    }

    /// <summary>
    /// 游戏状态变更事件，当游戏状态发生变更时触发。
    /// </summary>
    public static event Action<GameState> GameStateChangeEvent;

    /// <summary>
    /// 触发游戏状态变更事件。
    /// </summary>
    /// <param name="gameState">新的游戏状态。</param>
    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangeEvent?.Invoke(gameState);
    }
}
