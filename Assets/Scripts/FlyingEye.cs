using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;
using static MonsterTrash;

public class FlyingEye : MonoBehaviour
{
    public DetectionZone detectionZone;
    private Animator animator;
    private Damageable damageable;

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    Rigidbody2D body;

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

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
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
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            animator.SetBool(AnimationString.HasTarget, value);
            _hasTarget = value;
        }
    }

    public float AttackCooldown
    {
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
        HasTarget = detectionZone.detectedCollider.Count > 0;
        if (AttackCooldown > 0) AttackCooldown -= Time.deltaTime;
    }

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
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
}
