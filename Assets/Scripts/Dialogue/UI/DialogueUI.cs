using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public GameObject panel;
    public Text dialogueText;

    private void OnEnable()
    {
        EventHandle.ShowDialogueEvent += ShowDialogue;
    }

    private void OnDisable()
    {
        EventHandle.ShowDialogueEvent -= ShowDialogue;
    }
    

    private void ShowDialogue(string dialogue)
    {
        if (dialogue != String.Empty)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
        dialogueText.text = dialogue;
    }

}
