
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    public float walkingSpeed = 5f;
    public float runSpeed = 8f;
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private bool _isRunning = false;
    private float facingDirection = 1f;  // 1 for right, -1 for left

    Rigidbody2D PlayerRigidBody;
    Animator _animator;

    public float CurrentMoveSpeed
    {
        get
        {
            if (_isMoving)
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

    private void Awake()
    {
        PlayerRigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        // Ensure initial scale is set correctly
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void Update()
    {
        HandleFlipping();
    }

    private void FixedUpdate()
    {
        PlayerRigidBody.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, PlayerRigidBody.velocity.y);
        _animator.SetFloat(AnimationStrings.yVelocity, PlayerRigidBody.velocity.y);
    }

    private void HandleFlipping()
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
        IsMoving = moveInput != Vector2.zero;
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
}