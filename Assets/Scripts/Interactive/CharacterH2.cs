using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]
public class CharacterH2 : Interactive
{
    private DialogueController dialogueController;
    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }

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

    public override void OnclickedAction()
    {
        dialogueController.ShowDialogueFinish();
    }
}
