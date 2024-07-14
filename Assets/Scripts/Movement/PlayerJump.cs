using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private Vector3 jumpVector;
    [SerializeField] private bool canJump = true;
    [SerializeField] private float jumpForce = 1f;

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -9.8F, 0);

    }
    private void Update()
    {
        rigidbody.velocity = jumpVector;
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
        if (!canJump)
        {
            jumpForce = 0;
        }
        jumpVector = new Vector3(0, jumpForce, 0);
    }
}
