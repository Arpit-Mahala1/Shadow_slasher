
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    public float walkingSpeed = 5f;
    public float runSpeed = 8f;
    public float airSpeed = 3f;
    public float JumpImpulse=10f;
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private bool _isRunning = false;
    private float facingDirection = 1f;  // 1 for right, -1 for left

    Rigidbody2D PlayerRigidBody;
    Animator _animator;
    TouchingDirection touchingDirection;
    

    public float CurrentMoveSpeed
    {
        get
        {
            if (canMove)
            {
                if (_isMoving && !touchingDirection.IsOnWall)
                {
                    if (touchingDirection.IsGrounded)
                    {
                        if (_isRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkingSpeed;
                        }
                    }
                    else
                    {
                        return airSpeed;
                    }

                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
            
        }
    }

    public bool IsMoving
    {
        get { return _isMoving; }
        set
        {
            _isMoving = value;
            _animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool IsRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
            _animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool canMove
    {
        get
        {
            return _animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return _animator.GetBool(AnimationStrings.canMove);
        }
    }
    private void Awake()
    {
        PlayerRigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();

        // Ensure initial scale is set correctly
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void Update()
    {
        HandleFlipping(moveInput);
    }

    private void FixedUpdate()
    {
        PlayerRigidBody.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, PlayerRigidBody.velocity.y);
        _animator.SetFloat(AnimationStrings.yVelocity, PlayerRigidBody.velocity.y);
    }

    private void HandleFlipping(Vector2 moveInput)
    {
        if (moveInput.x != 0)
        {
            // If moving right
            if (moveInput.x > 0 && facingDirection != 1f)
            {
                facingDirection = 1f;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            // If moving left
            else if (moveInput.x < 0 && facingDirection != -1f)
            {
                facingDirection = -1f;
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            HandleFlipping(moveInput);
        }
        else
        {
            PlayerInput input = GetComponent<PlayerInput>(); input.actions.Disable();
        }
        
    }

    

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirection.IsGrounded && canMove)
        {
            _animator.SetTrigger(AnimationStrings.jump);
            PlayerRigidBody.velocity=new Vector2(PlayerRigidBody.velocity.x, JumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            _animator.SetTrigger(AnimationStrings.attack);
        }
    }
}