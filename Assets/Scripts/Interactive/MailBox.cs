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
    private void OnAfterSceneLoadEvent()
    {
        if (!isDone)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite = openSprite;
            boxCollider2D.enabled = false;
        }
    }

    public override void OnclickedAction()
    {
        spriteRenderer.sprite = openSprite;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
