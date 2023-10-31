using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;
using static MonsterTrash;
[RequireComponent(typeof(Rigidbody2D), typeof(Damageable), typeof(Animator))]

public class FlyingEye : MonoBehaviour
{
    public DetectionZone detectionZone;
    private Animator animator;
    private Damageable damageable;
    private Transform nextWayPoint;
    private int currentWayPoint = -1;

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public List<Transform> wayPoints;
    public float wayPointReachedDistance = 0.1f;
    public float flightSpeed = 3f;

    Rigidbody2D body;
    CapsuleCollider2D collider;

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationString.CanMove);
        }
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

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        NextWayPoint();
    }

    private void Update()
    {
        HasTarget = detectionZone.detectedCollider.Count > 0;
        if (AttackCooldown > 0) AttackCooldown -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove) Flight();
            else body.velocity = Vector3.zero;
        }
    }

    private void Flight()
    {
        Vector2 directionToWayPoint = (nextWayPoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextWayPoint.position, transform.position);

        body.velocity = directionToWayPoint * flightSpeed;

        if (distance <= wayPointReachedDistance) NextWayPoint();
        FlipDirection();
    }

    private void FlipDirection()
    {
        if (body.velocity.x > 0)
            WalkDirection = WalkableDirection.Right;
        else
            WalkDirection = WalkableDirection.Left;
    }

    private void NextWayPoint()
    {
        int countWayPoints = wayPoints.Count -1;
        if (countWayPoints == currentWayPoint) currentWayPoint = 0;
        else currentWayPoint ++;

        nextWayPoint = wayPoints[currentWayPoint];
    }

    public void OnTakeDamage(float damage, Vector2 knockback)
    {
        if (damageable.IsAlive) body.velocity = new Vector2(knockback.x, knockback.y * body.velocity.y);
    }

    public void OnDeath()
    {
        body.gravityScale = 2f;
        collider.offset = new Vector2(0, -0.47f); // We move the collider to match with the death animation
    }
}
