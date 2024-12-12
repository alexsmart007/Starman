using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using static DialogueHelperClass;

public class DialogueManager : SingletonMonoBehavior<DialogueManager>
{

    [SerializeField] float dialogueSpeed;
    [SerializeField] float dialogueFastSpeed;

    float currentDialogueSpeed;
    bool inDialogue;

    [Button]
    public void StartDialogue(SOConversationData conversation)
    {
        StartDialogue(conversation.Data.ID);
    }

    public void StartDialogue(string dialogueId)
    {
        if (dialogueId == null || dialogueId.ToLowerInvariant().Equals("exit"))
        {
            ExitDialogue();
            return;
        }
        else if (!inDialogue)
        {
            inDialogue = true;
            ProcessDialogue();
        }

        var SOConversationData = conversationGroup.Find(data => data.Data.ID.ToLower().Equals(dialogueId.ToLower()));
        if (SOConversationData == null)
        {
            Debug.LogError("Could not find " + dialogueId + " in database");
            return;
        }

        StartCoroutine(HandleConversation(SOConversationData.Data));
    }

        private void ExitDialogue()
    {
        inDialogue = false;
        OnDialogueEnded?.Invoke();
        Controller.Instance.SwapToGameplay();
    }

    private void SpeedUpText() => currentDialogueSpeed = currentDialogueSpeed == dialogueFastSpeed ? currentDialogueSpeed = dialogueFastSpeed * 10 : dialogueFastSpeed;
}
