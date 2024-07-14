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

    private bool inGameplay = true;
    public bool InGameplay => inGameplay;

    [SerializeField] PlayerInput playerInput;

    public void SwapToGameplay() { playerInput.SwitchCurrentActionMap("Gameplay"); inGameplay = true; }

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
            }
        }

        #endregion
    }
