using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{

    [SerializeField] private SOConversationData startingDialogue;

    void Start()
    {
        DialogueManager.Instance.StartDialogue(startingDialogue);
    }
}
