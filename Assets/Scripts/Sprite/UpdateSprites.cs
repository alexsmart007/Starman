using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Sirenix.OdinInspector;

public class UpdateSprites : SerializedMonoBehaviour
{

    [System.Serializable]
    private class SpriteData
    {
        public Animator spriteAnimator;
        public string Condition;
        public bool hasOccured;
    }

    [SerializeField] Dictionary<string, SpriteData> spriteData;

    void Start()
    {
        DialogueManager.OnDialogueEnded += UpdateSprite;
    }

    private void UpdateSprite()
    {
        foreach (var key in spriteData)
        {
            if (DialogueManager.Instance.unlocks.ContainsKey(key.Key.ToLower()) && !key.Value.hasOccured)
            {
                key.Value.spriteAnimator.SetBool(key.Value.Condition, true);
                key.Value.hasOccured = true;
            }
        }
    }

    void OnDestroy()
    {
        DialogueManager.OnDialogueEnded -= UpdateSprite;
    }
}