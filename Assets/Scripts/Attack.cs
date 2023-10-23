using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackDamage = 50f;
    public Vector2 knockback = new Vector2(0, 0);

    private void OnTriggerEnter2D(Collider2D entity)
    {
        Damageable damageable = entity.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockBack = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            damageable.Hit(attackDamage, deliveredKnockBack);
        }
    }
}
