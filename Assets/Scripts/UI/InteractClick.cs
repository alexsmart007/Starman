using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractClick : MonoBehaviour
{
    private Vector2 inputPositionVector;
    [SerializeField] private Collider2D newSelectionCollider = null;
    [SerializeField] private Texture2D interactiveCursorTexture;
    [SerializeField] private CurrentCollider currentCollider;
    [SerializeField] private SOConversationData dialogue;
    private Cursor cursor;
    private bool cursorIsInteractive = false;

    private void OnEnable()
    {
        InputManager.OnClick += Click;
    }

    private void OnDisable()
    {
        InputManager.OnClick -= Click;
    }

    void Click()
    {
        OnClickInteractable();
    }

    void OnMouseOver()
    {
        newSelectionCollider = this.GetComponent<Collider2D>();
        if (!cursorIsInteractive && InputManager.inGameplay) InteractiveCursorTexture();
        else if (currentCollider.currentSelectionCollider != newSelectionCollider)
        {
            currentCollider.currentSelectionCollider = newSelectionCollider;
            DefaultCursorTexture();
        }
    }

    void OnMouseExit()
    {
        newSelectionCollider = null;
        DefaultCursorTexture();
    }

    private void InteractiveCursorTexture()
    {
        cursorIsInteractive = true;
        Vector2 hotspot = new Vector2(interactiveCursorTexture.width / 2, 0);
        Cursor.SetCursor(interactiveCursorTexture, hotspot, CursorMode.Auto);
    }

    private void DefaultCursorTexture()
    {
        cursorIsInteractive = false;
        Cursor.SetCursor(default, default, default);
    }

    private void OnClickInteractable()
    {
        if (newSelectionCollider != null)
        {
            DefaultCursorTexture();
            DialogueManager.Instance.StartDialogue(dialogue.Data.ID);
        }
    }



}
