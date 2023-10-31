using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 moveSpeed = new Vector2(20, 0);
    public Vector2 knockback = new Vector2(0, 0);
    public float damage = 10f;

    Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Start()
    {
        body.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D entity)
    {
        Damageable damageable = entity.GetComponent<Damageable>();
        if (damageable)
        {
            if (damageable.IsAlive)
            {
                Vector2 deliveredKnockBack = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                damageable.Hit(damage, deliveredKnockBack);
                Destroy(gameObject);
            }
        }
    }
}
