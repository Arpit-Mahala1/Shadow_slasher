using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    Animator animator;
    [SerializeField]
    private float _maxHealth=100;
    [SerializeField]
    private float _health=100f;
    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit=0;
    public float InvincibleTime=0.25f;

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

    

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if( _health < 0 )
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
        Hit(10);
    }

    public void Hit(int damage)
    {
        if( _isAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
        } 


    }
}
