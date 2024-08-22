using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private float halfPlayerHeight;
    private bool letPlayerJump = false;
    private Vector3 jumpVelocity;
    [SerializeField] private bool canJump = true;
    [SerializeField] private float jumpHeight = 2f;
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
        if(letPlayerJump)
        {
            rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
            letPlayerJump = false;
        }
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
        jumpVelocity = (canJump && isGrounded()) ? Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y) : Vector3.zero;
        letPlayerJump = true;
    }

    private bool isGrounded()
    {
        Vector3 down = transform.TransformDirection(Vector3.down);
        Debug.DrawRay(transform.position, new Vector3(0, -halfPlayerHeight, 0), Color.green, 1000);
        return Physics.Raycast(transform.position, down, halfPlayerHeight, ~playerMask);
    }
}
