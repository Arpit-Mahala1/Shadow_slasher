//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class Damageable : MonoBehaviour
//{

//    public UnityEvent<int, Vector2> damageableHit;


//    Animator animator;
//    [SerializeField]
//    private float _maxHealth=100;
//    [SerializeField]
//    private float _health=100f;
//    [SerializeField]
//    private bool _isAlive = true;
//    [SerializeField]
//    private bool isInvincible = false;
//    private float timeSinceHit=0;
//    public float InvincibleTime=0.25f;

//    public float MaxHealth
//    {
//        get
//        {
//            return _maxHealth;
//        }
//        set
//        {
//            _maxHealth = value;
//        }
//    }

//    public bool LockVelocity
//    {
//        get
//        {
//            return animator.GetBool(AnimationStrings.lockVelocity);
//        }
//        set
//        {
//            animator.SetBool(AnimationStrings.lockVelocity, value);
//        }
//    }

//    public float Health
//    {
//        get
//        {
//            return _health;
//        }
//        set
//        {
//            _health = value;

//            if( _health <= 0 )
//            {
//                IsAlive = false;
//            }
//        }
//    }


//    public bool IsAlive
//    {
//        get
//        {
//            return _isAlive;
//        }
//        set
//        {
//            _isAlive = value;
//            animator.SetBool(AnimationStrings.isAlive, value);
//        }
//    }

//    private void Awake()
//    {
//        animator = GetComponent<Animator>();
//    }

//    private void Update()
//    {
//        if (isInvincible)
//        {
//            if (timeSinceHit > InvincibleTime)
//            {
//                isInvincible = false;
//                timeSinceHit = 0;
//            }
//            timeSinceHit += Time.deltaTime;
//        }
//    }

//    public bool Hit(int damage, Vector2 knockback)
//    {
//        if( _isAlive && !isInvincible)
//        {
//            Health -= damage;
//            isInvincible = true;

//            LockVelocity=true;
//            animator.SetTrigger(AnimationStrings.hitTrigger);
//            damageableHit?.Invoke(damage, knockback);


//            return true;
//        } 
//        return false;

//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    Animator animator;
    [SerializeField]
    private float _maxHealth = 100;
    [SerializeField]
    private float _health = 100f;
    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    public float InvincibleTime = 0.25f;

    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            Debug.Log($"{gameObject.name}: Setting LockVelocity to {value}");
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log($"{gameObject.name}: IsAlive set to {value}");
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > InvincibleTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }

        // Force unlock after hit animation completes
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (LockVelocity && !stateInfo.IsName("Player_hit"))
        {
            Debug.Log($"{gameObject.name}: Not in hit state but locked - forcing unlock");
            LockVelocity = false;
        }

        // Debug current state
        if (LockVelocity)
        {
            Debug.Log($"{gameObject.name}: LockVelocity is TRUE, canMove: {animator.GetBool(AnimationStrings.canMove)}, Current State: {stateInfo.IsName("Player_hit")}");
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (_isAlive && !isInvincible)
        {
            Debug.Log($"{gameObject.name}: Got hit! Damage: {damage}");
            Health -= damage;
            isInvincible = true;
            LockVelocity = true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            damageableHit?.Invoke(damage, knockback);

            return true;
        }
        return false;
    }
}