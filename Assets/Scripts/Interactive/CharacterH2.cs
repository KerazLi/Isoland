using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CharacterH2 类继承自 Interactive，用于处理角色对话交互。
// 它需要一个 DialogueController 组件来控制对话的显示。
[RequireComponent(typeof(DialogueController))]
public class CharacterH2 : Interactive
{
    // 存储 DialogueController 的实例，用于对话控制。
    private DialogueController dialogueController;

    // Awake 方法在对象激活时调用，用于初始化 dialogueController。
    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }

    // EmptyClicked 方法用于处理空点击事件。
    // 当对话已完成或未开始时，显示不同的对话界面。
    public override void EmptyClicked()
    {
        if (isDone)
        {
            dialogueController.ShowDialogueFinish();
        }
        else
        {
            dialogueController.ShowDialogueEmpty();
        }
    }

    // OnclickedAction 方法用于处理点击事件。
    // 当角色被点击时，显示对话结束界面。
    public override void OnclickedAction()
    {
        dialogueController.ShowDialogueFinish();
    }
}