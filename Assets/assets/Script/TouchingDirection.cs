using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    public float groundDistance = 0.05f;
    public ContactFilter2D castFilter;

    CapsuleCollider2D touchingCol;
    Animator animator;

    RaycastHit2D[] groundHit=new RaycastHit2D[5];

    private bool _isGrounded;

    public bool IsGrounded { get { 
            return _isGrounded;
        } private set {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        } }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHit, groundDistance) > 0;

    }
}
