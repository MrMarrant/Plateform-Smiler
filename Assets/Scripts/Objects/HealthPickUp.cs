using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float healthRestore = 20f;
    public Vector3 spinRotationSpeed = new Vector3(0, 100, 0);
    AudioSource audioPickUp;

    private void Awake()
    {
        audioPickUp = GetComponent<AudioSource>();
    }

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
            if (healSeted)
            {
                AudioSource.PlayClipAtPoint(audioPickUp.clip, gameObject.transform.position, audioPickUp.volume);
                Destroy(gameObject);
            }
        }
    }
}
