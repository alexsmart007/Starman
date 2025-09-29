using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractClick : MonoBehaviour
{
    private Vector2 inputPositionVector;
    [SerializeField] private Collider2D newSelectionCollider = null;
    [SerializeField] private CurrentCollider currentCollider;
    [SerializeField] private SOConversationData dialogue;
    [SerializeField] private Animator Hand;
    [SerializeField] private bool isBag;
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
        CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Grab);
    }

    private void DefaultCursorTexture()
    {
        cursorIsInteractive = false;
        CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.Arrow);
    }

    private void OnClickInteractable()
    {
        if (newSelectionCollider != null)
        {
            DefaultCursorTexture();
            if (!isBag)
            {
                DialogueManager.Instance.StartDialogue(dialogue.Data.ID);
            }
            else
            {

            }
        }
    }



}
