using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using static DialogueHelperClass;

public class DialogueController : MonoBehaviour
{
    [SerializeField] TextBoxDisplay textBoxDisplay;

    private void OnEnable()
    {
        DialogueManager.OnDialogueStarted += DisplayUI;
        DialogueManager.OnDialogueEnded += HideUI;
        HideUI();
    }

    private void HideUI()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        textBoxDisplay.Hide();
    }

    private void DisplayUI(ConversationData conversation)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        textBoxDisplay.Display();
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStarted -= DisplayUI;
        DialogueManager.OnDialogueEnded -= HideUI;
    }
}
