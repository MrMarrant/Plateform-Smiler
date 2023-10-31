using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouController : MonoBehaviour
{
    public Camera camera;
    public Transform TransformYou { get; private set; }
    void Awake()
    {
        TransformYou = GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        TransformYou.position = camera.transform.position;
    }
}
