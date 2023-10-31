using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;
using static MonsterTrash;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class MonsterTrash : MonoBehaviour
{
    public enum WalkableDirection { Right, Left }

    public float walStopRate = 0.05f;
    public float walkAcceleration = 30f;
    public float maxSpeed = 3f;

    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    private Animator animator;
    private Damageable damageable;

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    Rigidbody2D body;
    TouchingDirections touchingDirections;
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationString.CanMove);
        }
    }
    private bool IsAlive
    {
        get { return animator.GetBool(AnimationString.IsAlive); }
    }

    public WalkableDirection WalkDirection {
        get { return _walkDirection; }
        set { 
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right) walkDirectionVector = Vector2.right;
                else if (value == WalkableDirection.Left) walkDirectionVector = Vector2.left;
            }
            _walkDirection = value;
        }
    }

    [SerializeField]
    private bool _hasTarget = false;
    public bool HasTarget { 
        get { return _hasTarget; } 
        private set {
            animator.SetBool(AnimationString.HasTarget, value);
            _hasTarget = value;
        } 
    }

    public float AttackCooldown { 
        get
        {
            return animator.GetFloat(AnimationString.AttackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationString.AttackCooldown, Mathf.Max(value, 0));
        }
    }

    private void Update()
    {
        HasTarget = attackZone.detectedCollider.Count > 0;
        if (AttackCooldown> 0 ) AttackCooldown -= Time.deltaTime;
    }
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (touchingDirections.IsOnGround && touchingDirections.IsOnWall) FlipDirection();

        if (!damageable.LockVelocity)
        {
            if (CanMove && touchingDirections.IsOnGround)
            {
                body.velocity = new Vector2(Mathf.Clamp(body.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), 
                    -maxSpeed, maxSpeed), body.velocity.y);
            }
            else body.velocity = new Vector2(Mathf.Lerp(body.velocity.x, 0, walStopRate), body.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right) 
            WalkDirection = WalkableDirection.Left;
        else if (WalkDirection == WalkableDirection.Left) 
            WalkDirection = WalkableDirection.Right;
        else 
            Debug.LogError("Didnt manage other direction yet");
    }

    public void OnTakeDamage(float damage, Vector2 knockback)
    {
        if (IsAlive) body.velocity = new Vector2(knockback.x, knockback.y * body.velocity.y);
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.IsOnGround) FlipDirection();
    }
}
