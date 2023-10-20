using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static MonsterTrash;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class MonsterTrash : MonoBehaviour
{

    public float walkSpeed = 3f;
    Rigidbody2D body;
    TouchingDirections touchingDirections;
    public DetectionZone detectionZone;
    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

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
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (touchingDirections.IsOnWall && touchingDirections.IsOnGround) FlipDirection();
        body.velocity = new Vector2(walkDirectionVector.x * walkSpeed, body.velocity.y);
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
}
