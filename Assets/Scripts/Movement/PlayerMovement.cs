using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private new Rigidbody rigidbody;
    private Vector3 movementForce;
    private Vector2 playerInput;
    [SerializeField] private bool canMove = true;
    [SerializeField] private float speed = 0.25f;

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();

    }
    private void Update()
    {
        rigidbody.MovePosition(rigidbody.position + movementForce * speed * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        InputManager.OnMove += Move;
    }

    private void OnDisable()
    {
        InputManager.OnMove -= Move;
    }

    void Move(Vector2 input)
    {
        if (!canMove)
        {
            input = Vector2.zero;
        }
        playerInput = input;
        movementForce = new Vector3(input.x, 0, input.y);
    }
}
