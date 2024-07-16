using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 对话控制器类，负责管理游戏中的对话显示。
 */
public class DialogueController : MonoBehaviour
{
    /**
     * 空对话数据对象，用于对话开始前的占位。
     */
    public DialogueDataSO dialogueEmpty;
    /**
     * 结束对话数据对象，用于对话结束后显示。
     */
    public DialogueDataSO dialogueFinish;
    /**
     * 标记当前是否正在显示对话。
     */
    private bool isTalking;
    /**
     * 存储空对话数据的栈，用于对话开始。
     */
    private Stack<string> dialogueEmptyStack;
    /**
     * 存储结束对话数据的栈，用于对话结束。
     */
    private Stack<string> dialogueFinishStack;

    /**
     * 初始化函数，在对象激活时调用，用于填充对话栈。
     */
    private void Awake()
    {
        FillDialogueStack();
    }

    /**
     * 填充对话栈的函数，分别填充空对话和结束对话的栈。
     */
    private void FillDialogueStack()
    {
        dialogueEmptyStack = new Stack<string>();
        dialogueFinishStack = new Stack<string>();
        // 倒序填充空对话栈
        for (int i = dialogueEmpty.dialogueList.Count - 1; i >= 0; i--)
        {
            dialogueEmptyStack.Push(dialogueEmpty.dialogueList[i]);
        }
        // 倒序填充结束对话栈
        for (int i = dialogueFinish.dialogueList.Count - 1; i >= 0; i--)
        {
            dialogueFinishStack.Push(dialogueFinish.dialogueList[i]);
        }
    }

    /**
     * 显示空对话的函数，如果当前没有对话正在进行，则开始显示空对话。
     */
    public void ShowDialogueEmpty()
    {
        if (!isTalking)
        {
            StartCoroutine(DialogueRountine(dialogueEmptyStack));
        }
    }
    
    /**
     * 显示结束对话的函数，如果当前没有对话正在进行，则开始显示结束对话。
     */
    public void ShowDialogueFinish()
    {
        if (!isTalking)
        {
            StartCoroutine(DialogueRountine(dialogueFinishStack));
        }
    }

    /**
     * 显示对话的协程，负责从栈中取出对话并显示，直到栈为空。
     * @param data 存储对话的栈。
     */
    private IEnumerator DialogueRountine(Stack<string> data)
    {
        isTalking = true;
        // 如果栈不为空，则取出对话并显示
        while (data.TryPop(out string result))
        {
            EventHandle.CallShowDialogueEvent(result);
            yield return null; // 等待一帧
        }
        // 对话结束后的处理
        isTalking = false;
        if (data.Count == 0)
        {
            EventHandle.CallShowDialogueEvent(String.Empty); // 显示空对话
            FillDialogueStack(); // 重新填充对话栈
            EventHandle.CallGameStateChangeEvent(GameState.GamePlay); // 改变游戏状态为进行中
        }
    }

}
