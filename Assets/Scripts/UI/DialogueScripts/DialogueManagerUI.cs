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

    public static Action<ConversationData> OnDialogueStarted;
    public static Action OnDialogueEnded;
    public static Action<string> OnTextUpdated;

    [SerializeField, ReadOnly] List<SOConversationData> conversationGroup;

    float currentDialogueSpeed;
    bool inDialogue;
    bool continueInputRecieved;

    protected override void Awake()
    {
        base.Awake();
        conversationGroup = Resources.LoadAll<SOConversationData>("Dialogue").ToList();
    }

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
            InputManager.Instance.SwapToUI();
        }

        var SOConversationData = conversationGroup.Find(data => data.Data.ID.ToLower().Equals(dialogueId.ToLower()));
        if (SOConversationData == null)
        {
            Debug.LogError("Could not find " + dialogueId + " in database");
            return;
        }

        StartCoroutine(HandleConversation(SOConversationData.Data));
    }

    private IEnumerator HandleConversation(ConversationData data)
    {
        OnDialogueStarted?.Invoke(data);

        if (data.Dialogues.Count >= 1 && !data.Dialogues[0].Dialogue.IsNullOrWhitespace())
        {
            foreach (var dialogue in data.Dialogues)
            {
                yield return ProcessDialogue(dialogue, data.Conversant);
            }
        }
        StartDialogue(data.NextDialogueID);
    }

    private IEnumerator ProcessDialogue(DialogueData dialogue, string conversant)
    {
        OnTextUpdated?.Invoke("");
        string name = "";
        continueInputRecieved = false;
        if (!dialogue.VoiceSpeaker)
        {
            name = "<u>" + (dialogue.PlayerIsSpeaker ? "Player" : (conversant + ": ")) + "</u>\n";
        }

        yield return TypewriterDialogue(name, dialogue.Dialogue, dialogue.PlayerIsSpeaker);

        InputManager.OnNextDialogue += OnContinueInput;

        yield return new WaitUntil(() => continueInputRecieved);

        Debug.Log("yooo");

        InputManager.OnNextDialogue -= OnContinueInput;
    }

    private void ExitDialogue()
    {
        inDialogue = false;
        OnDialogueEnded?.Invoke();
        InputManager.Instance.SwapToGameplay();
    }

    private IEnumerator TypewriterDialogue(string name, string line, bool isPlayerSpeaker)
    {
        currentDialogueSpeed = dialogueSpeed;
        string loadedText = name;
        InputManager.OnNextDialogue += SpeedUpText;
        bool atSpecialCharacter = false;
        foreach (char letter in line)
        {
            loadedText += letter;
            atSpecialCharacter = letter == '<' || atSpecialCharacter;
            if (atSpecialCharacter && letter != '>') continue;
            atSpecialCharacter = false;
            OnTextUpdated?.Invoke(loadedText);
            yield return new WaitForSeconds(1 / currentDialogueSpeed);
        }
        InputManager.OnNextDialogue -= SpeedUpText;
    }

    private void OnContinueInput() => continueInputRecieved = true;

    private void SpeedUpText() => currentDialogueSpeed = currentDialogueSpeed == dialogueFastSpeed ? currentDialogueSpeed = dialogueFastSpeed * 10 : dialogueFastSpeed;
}
