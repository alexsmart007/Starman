using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private new CharacterController controller;
    private Vector3 jumpVector;
    private float halfPlayerHeight;
    private bool grounded = false;
    private Vector3 velocity;
    [SerializeField] private bool canJump = true;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float forceOfGravity = -9.8f;
    [SerializeField] private LayerMask playerMask; 

    private void Start()
    {
        controller = this.GetComponent<CharacterController>();
        Physics.gravity = new Vector3(0, forceOfGravity, 0);
        halfPlayerHeight = (transform.lossyScale.y / 2);
    }

    private void Update()
    {
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnEnable()
    {
        InputManager.OnJump += Jump;
    }

    private void OnDisable()
    {
        InputManager.OnJump -= Jump;
    }

    void Jump()
    {
        //condition ? consequent : alternative
        jumpVector = (canJump && isGrounded()) ? new Vector3(0, jumpForce, 0) : Vector3.zero;
        grounded = false;
    }

    private bool isGrounded()
    {
        Vector3 down = transform.TransformDirection(Vector3.down);
        Debug.DrawRay(transform.position, new Vector3(0, -halfPlayerHeight, 0), Color.green, 1000);
        if (Physics.Raycast(transform.position, down, halfPlayerHeight, ~playerMask))
        {
            grounded = true;
            print("They are grounded");
        }
        return grounded;
    }
}
