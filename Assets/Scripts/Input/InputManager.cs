using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : SingletonMonoBehavior<InputManager>
{
    public static Action<Vector2> OnMove;
    public static Action<Vector2> OnRotateCamera;
    public static Action OnJump;
    public static Action<InputAction.CallbackContext> OnStarDash;

    public static Action OnClick;
    public static Action OnNextDialogue;
    public static Action OnSelect;

    public static bool inGameplay = true;
    public bool InGameplay => inGameplay;

    [SerializeField] PlayerInput playerInput;

    public void SwapToUI() { playerInput.SwitchCurrentActionMap("UI"); inGameplay = false; Debug.Log("In UI Now"); }
    public void SwapToGameplay() { playerInput.SwitchCurrentActionMap("Gameplay"); inGameplay = true; Debug.Log("In Game Now"); }

    #region Gameplay Layout

    public void Move(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context.ReadValue<Vector2>());
    }

     public void RotateCamera(InputAction.CallbackContext context)
     {
        OnRotateCamera?.Invoke(context.ReadValue<Vector2>());
     }

     public void Jump(InputAction.CallbackContext context)
     {
        if (context.started)
        {
            OnJump?.Invoke();
            OnStarDash?.Invoke(context);
        }
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnClick?.Invoke();
        }
    }

    #endregion

    #region UI Layout

    public void NextDialogue(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnNextDialogue?.Invoke();
        }
    }

    public void Select(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnSelect?.Invoke();
        }
    }

    #endregion
}
