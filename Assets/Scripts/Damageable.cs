using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Events;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public Animator animator;
    public UnityEvent<float, Vector2> OnTakeDamage;
    public UnityEvent<float> OnHeal;
    public UnityEvent OnDeath;
    public UnityEvent<float, float> HealthChanged;

    [SerializeField]
    private float _maxHealth = 100;
    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField]
    private float _health = 100;
    public float Health
    {
        get { return _health; }
        set { 
            _health = Mathf.Clamp(value, 0, MaxHealth);
            HealthChanged?.Invoke(_health, MaxHealth);
            if (_health <= 0) { 
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationString.IsAlive, value);
            if (!value) OnDeath.Invoke();
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationString.LockVelocity);
        }
        set
        {
            animator.SetBool(AnimationString.LockVelocity, value);
        }
    }

    private float timeSinceHit = 0;
    public float invicibilityTime = 0.25f;

    [SerializeField]
    private bool _isInvicible = false;
    public bool IsInvicible
    {
        get { return _isInvicible; }
        set { _isInvicible = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsInvicible)
        {
            if (timeSinceHit > invicibilityTime)
            {
                IsInvicible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(float damage, Vector2 knockback)
    {
        if(IsAlive && !IsInvicible)
        {
            Health -= damage;
            IsInvicible = true;
            LockVelocity= true;
            animator.SetTrigger(AnimationString.HitTrigger);
            OnTakeDamage?.Invoke(damage, knockback);
            CharacterEvent.CharacterOnDamage.Invoke(gameObject, damage);
            return true;
        }
        return false;
    }
    public bool Heal(float healthValue)
    {
        if (IsAlive && Health < MaxHealth)
        {
            Health += healthValue;
            OnHeal?.Invoke(healthValue);
            CharacterEvent.CharacterOnHeal.Invoke(gameObject, healthValue);
            return true;
        }
        return false;
    }

}
