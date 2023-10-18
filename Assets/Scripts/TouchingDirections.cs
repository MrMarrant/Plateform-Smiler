using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D contactFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isOnGround;
    public bool IsOnGround { get
        {
            return _isOnGround;
        }
        private set
        {
            _isOnGround = value;
            animator.SetBool(AnimationString.IsOnGround, value);
        }
    }

    [SerializeField]
    private bool _isOnWall;
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationString.IsOnWall, value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling;
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationString.IsOnCeiling, value);
        }
    }

    CapsuleCollider2D capsuleCollider;
    Animator animator;
    Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        IsOnGround = capsuleCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        IsOnWall = capsuleCollider.Cast(wallCheckDirection, contactFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = capsuleCollider.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }

    // On set le bool landing le temps que l'animation se joue
    // TODO : Get le temps restant de l'animation plutot
    /*IEnumerator SetLanding()
    {
        animator.SetBool(AnimationString.IsLanding, true);
        yield return new WaitForSeconds(0.5f); // waits 0.5 seconds
        animator.SetBool(AnimationString.IsLanding, false);
    }*/
}
