using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public static class DialogueHelperClass
{
    public static readonly string ID_MARKER = "ID: ";
    public static readonly string CONVERSANT_MARKER = "Conversant: ";
    public static readonly string DIALOGUE_MARKER = "Dialogue:";
    public static readonly string LEADS_TO_MARKER = "Leads to:";

    [System.Serializable]
    public class DialogueData
    {
        public bool PlayerIsSpeaker;
        public bool VoiceSpeaker;
        [SerializeField, TextArea()] public string Dialogue;
    }

    [System.Serializable]
    public class ConversationData
    {
        public string ID;
        public string Conversant;
        public string NextDialogueID;

        public bool NewDialogueOnReClick;
        [NaughtyAttributes.ShowIf("NewDialogueOnReClick")]
        [NaughtyAttributes.AllowNesting]
        public string NextNewDialogueID;

        public List<DialogueData> Dialogues = new List<DialogueData>();
    }


}
