using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class DialogueHelperClass
{
    public static readonly string ID_MARKER = "ID: ";
    public static readonly string CONVERSANT_MARKER = "Conversant: ";
    public static readonly string DIALOGUE_MARKER = "Dialogue:";
    public static readonly string LEADS_TO_MARKER = "Leads to:";

    [System.Serializable]
    public class ConversationData
    {
        public string ID;
        public string Conversant;
        public string NextDialogueID;
    }
}
