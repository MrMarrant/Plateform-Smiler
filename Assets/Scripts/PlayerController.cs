using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    public float walkSpeed = 8f;
    public float AirSpeed = 4f;
    public float crouchSpeed = 3f;
    public float jumpImpulse = 10f;
    private float CurrentMoveSpeed { 
        get
        {   if (IsMoving && !touchingDirections.IsOnWall && CanMove)
            {
                if (!touchingDirections.IsOnGround) return AirSpeed;
                if (IsCrouch) return crouchSpeed;

                return walkSpeed;
            }

            return 0;
        }
    }

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving { get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            Anim.SetBool(AnimationString.IsMoving, value);
        }
    }

    [SerializeField]
    private bool _isCrouch = false;
    public bool IsCrouch
    {
        get
        {
            return _isCrouch;
        }
        private set
        {
            _isCrouch = value;
            Anim.SetBool(AnimationString.IsCrouch, value);
        }
    }
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Anim { get; private set; }

    private bool _isFacingRight = true;
    public bool IsFacingRight { get
        {
            return _isFacingRight;
        }
            private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return Anim.GetBool(AnimationString.CanMove);
        }
    }

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = new Vector2(CurrentMoveSpeed * moveInput.x, Rigidbody.velocity.y);
        Anim.SetFloat(AnimationString.YVelocity, Rigidbody.velocity.y);
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started) // context.started = Touche Appuyé
        {
            IsCrouch = true;
        }
        else if (context.canceled) // context.started = Touche Relaché
        {
            IsCrouch = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsOnGround && CanMove) // context.started = Touche Appuyé
        {
            Anim.SetTrigger(AnimationString.JumpTrigger);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsOnGround) // context.started = Touche Appuyé
        {
            Anim.SetTrigger(AnimationString.AttackTrigger);
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
}
