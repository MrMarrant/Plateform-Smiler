using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{

    public List<Collider2D> detectedCollider = new List<Collider2D>();
    public UnityEvent noCollidersRemain;
    Collider2D body;
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedCollider.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedCollider.Remove(collision);

        if (detectedCollider.Count <= 0) noCollidersRemain.Invoke();
    }
}
