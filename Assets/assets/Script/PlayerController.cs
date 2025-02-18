using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    public float walkingSpeed = 5f;
    public bool isMoving { get; private set; }
    Rigidbody2D PlayerRigidBody;

    private void Awake()
    {
        PlayerRigidBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerRigidBody.velocity = new Vector2(moveInput.x * walkingSpeed, PlayerRigidBody.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput=context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;
    }
}
