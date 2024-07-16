using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 邮箱类，继承自Interactive，表示一个可以交互的邮箱对象。
/// </summary>
public class MailBox : Interactive
{
    /// <summary>
    /// 箱子的精灵渲染器，用于改变邮箱的外观。
    /// </summary>
    private SpriteRenderer spriteRenderer;
    /// <summary>
    /// 箱子的2D盒碰撞器，用于检测与其它物体的碰撞。
    /// </summary>
    private BoxCollider2D boxCollider2D;
    /// <summary>
    /// 邮箱打开时的精灵，用于视觉效果。
    /// </summary>
    public Sprite openSprite;

    /// <summary>
    /// 初始化组件，包括精灵渲染器和盒碰撞器。
    /// </summary>
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// 当物体启用时，注册场景加载后的事件处理函数。
    /// </summary>
    private void OnEnable()
    {
        EventHandle.AfterSceneLoadEvent += OnAfterSceneLoadEvent;
    }

    /// <summary>
    /// 当物体禁用时，取消注册场景加载后的事件处理函数。
    /// </summary>
    private void OnDisable()
    {
        EventHandle.AfterSceneLoadEvent -= OnAfterSceneLoadEvent;
    }

    /// <summary>
    /// 场景加载后事件的处理函数。
    /// 根据isDone的值决定是否显示邮箱为打开状态，并隐藏或显示子对象。
    /// </summary>
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
    /// 点击交互动作的处理函数。
    /// 改变邮箱的外观为打开状态，并使第一个子对象变为活跃状态。
    /// </summary>
    /// <param name="info">交互信息，此处未使用。</param>
    public override void OnclickedAction()
    {
        // 更换按钮的精灵图像为打开状态的图像。
        spriteRenderer.sprite = openSprite;
        
        // 激活按钮下的第一个子对象，使其可见并可用。
        transform.GetChild(0).gameObject.SetActive(true);
    }
}