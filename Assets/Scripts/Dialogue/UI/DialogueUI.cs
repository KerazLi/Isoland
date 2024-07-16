using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/**
 * 对话UI管理类，负责显示游戏中的对话内容。
 */
public class DialogueUI : MonoBehaviour
{
    /**
     * 对话面板的GameObject，用于控制对话框的显示和隐藏。
     */
    public GameObject panel;
    /**
     * 用于显示对话文本的Text组件。
     */
    public Text dialogueText;

    /**
     * 组件启用时注册显示对话事件的监听。
     */
    private void OnEnable()
    {
        EventHandle.ShowDialogueEvent += ShowDialogue;
    }

    /**
     * 组件禁用时取消显示对话事件的监听。
     */
    private void OnDisable()
    {
        EventHandle.ShowDialogueEvent -= ShowDialogue;
    }

    /**
     * 显示对话内容。
     *
     * @param dialogue 要显示的对话文本。
     */
    private void ShowDialogue(string dialogue)
    {
        // 根据对话内容是否为空，控制对话面板的激活状态
        if (dialogue != String.Empty)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
        // 更新对话文本内容
        dialogueText.text = dialogue;
    }
}