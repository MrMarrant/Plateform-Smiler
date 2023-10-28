using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float healthRestore = 20f;
    public Vector3 spinRotationSpeed = new Vector3(0, 100, 0);
    // Start is called before the first frame update
    void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable)
        {
            bool healSeted = damageable.Heal(healthRestore);
            if (healSeted) Destroy(gameObject);
        }
    }
}
