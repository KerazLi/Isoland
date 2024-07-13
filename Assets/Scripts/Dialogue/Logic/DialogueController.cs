using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueDataSO dialogueEmpty;
    public DialogueDataSO dialogueFinish;
    private bool isTalking;
    private Stack<string> dialogueEmptyStack;
    private Stack<string> dialogueFinishStack;

    private void Awake()
    {
        FillDialogueStack();
    }

    private void FillDialogueStack()
    {
        dialogueEmptyStack = new Stack<string>();
        dialogueFinishStack = new Stack<string>();
        for (int i = dialogueEmpty.dialogueList.Count-1; i >=0; i--)
        {
            dialogueEmptyStack.Push(dialogueEmpty.dialogueList[i]);
        }
        for (int i = dialogueFinish.dialogueList.Count-1; i >=0; i--)
        {
            dialogueFinishStack.Push(dialogueFinish.dialogueList[i]);
        }
    }

    public void ShowDialogueEmpty()
    {
        if (!isTalking)
        {
            StartCoroutine(DialogueRountine(dialogueEmptyStack));
        }
    }
    public void ShowDialogueFinish()
    {
        if (!isTalking)
        {
            StartCoroutine(DialogueRountine(dialogueFinishStack));
        }
    }

    private IEnumerator DialogueRountine(Stack<string> data)
    {
        isTalking = true;
        if (data.TryPop(out string result))
        {
            EventHandle.CallShowDialogueEvent(result);
            yield return null;
            isTalking = false;
            EventHandle.CallGameStateChangeEvent(GameState.Pause);
        }
        else
        {
            EventHandle.CallShowDialogueEvent(String.Empty);
            FillDialogueStack();
            isTalking = false;
            EventHandle.CallGameStateChangeEvent(GameState.GamePlay);
        }
    }

}
