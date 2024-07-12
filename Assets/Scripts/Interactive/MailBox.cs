using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : Interactive
{
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    public Sprite openSprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        EventHandle.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
    }
    

    private void OnDisable()
    {
        EventHandle.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
    }
    /// <summary>
    /// 场景加载后事件的响应函数。
    /// </summary>
    /// <remarks>
    /// 此函数用于处理场景加载后的特定逻辑。它首先检查一个标志（isDone）以决定是隐藏子对象还是启用特定的精灵和碰撞器。
    /// 这种设计可能用于实现一种开/关状态机制，或者用于在场景加载后的特定条件下设置对象的状态。
    /// </remarks>
    private void OnAfterSceneLoadEvent()
    {
        // 如果isDone标志为false，隐藏子对象。这可能是一种避免过早交互或显示未完成内容的机制。
        if (!isDone)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            // 当isDone标志为true时，设置精灵渲染器的精灵为“打开”状态的精灵，并禁用2D盒碰撞器。
            // 这可能表示对象现在处于可交互或可见的状态。
            spriteRenderer.sprite = openSprite;
            boxCollider2D.enabled = false;
        }
    }


    /// <summary>
    /// 当按钮被点击时执行的动作。
    /// </summary>
    /// <remarks>
    /// 此方法旨在改变按钮的外观和行为以响应点击事件。它通过更换按钮的精灵图像来表示按钮被打开的状态，
    /// 并激活按钮下的一个子对象，这可能是一个显示更多内容的面板或一个动画效果。
    /// </remarks>
    public override void OnclickedAction()
    {
        // 更换按钮的精灵图像为打开状态的图像。
        spriteRenderer.sprite = openSprite;
        
        // 激活按钮下的第一个子对象，使其可见并可用。
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
