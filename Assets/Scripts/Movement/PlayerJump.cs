using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private Vector3 jumpVector;
    private float halfPlayerHeight;
    private bool grounded = false;
    [SerializeField] private bool canJump = true;
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private float forceOfGravity = -9.8f;
    [SerializeField] private LayerMask playerMask; 

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, forceOfGravity, 0);
        halfPlayerHeight = (transform.lossyScale.y / 2);
    }

    private void Update()
    {

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
        rigidbody.AddForce(jumpVector, ForceMode.Force);
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
