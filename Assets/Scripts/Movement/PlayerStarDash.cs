using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStarDash : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private InputAction.CallbackContext Context;
    [SerializeField] double timeHeldToStartDash;

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        print(Context.duration);
        if (Context.duration > timeHeldToStartDash)
        {
            print("Dash! " + Context.duration);
        }
    }

    private void OnEnable()
    {
        InputManager.OnStarDash += StarDash;
    }

    private void OnDisable()
    {
        InputManager.OnStarDash -= StarDash;
    }

    void StarDash(InputAction.CallbackContext context)
    {
        Context = context;
    }

}
